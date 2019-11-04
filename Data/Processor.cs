using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
//
using Newtonsoft.Json;
//
namespace Data
{
    public class Processor:IProcessor
    {
        private readonly Data.IGetter _Getter;
        private readonly Logger.ILogger _Logger;
        private IEnumerable<Models.Pizza> _pizza;

        public Processor(Data.IGetter getter, Logger.ILogger logger)
        {
            _Getter = getter ?? throw new System.ArgumentNullException("getter is missing");
            _Logger = logger ?? throw new System.ArgumentNullException("logger is missing");
        }

        public async Task<IEnumerable<Models.Pizza>> PopulateDataAsync()
        {
            var result = await _Getter.GetDataAsync().ConfigureAwait(false);
            if (result.Success)
            {
                var data = JsonConvert.DeserializeObject<List<Models.Pizza>>(result.Result.ToString());
                _Logger.LogInfo($" received {data.Count} pizza entries from {_Getter.DataUrl}");

                _pizza = data;
            }
            else
            {
                //throw new System.InvalidOperationException($"error pulling pizza data {result.Exception.Message}");
                _pizza = Models.Pizza.NoPizza;
            }

            return _pizza;
        }

        /// <summary>
        ///     frequently ordered pizza topping combinations
        ///     List the toppings for each popular pizza topping combination along with its rank and the number of times that combination has been ordered.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public IEnumerable<Models.Pizza> PullTopNEntries(int n)
        {
            if(_pizza == null)
            {
                throw new System.ArgumentNullException("pizza is missing");
            }
            //top 20 most frequently ordered pizza topping combinations
            //List the toppings for each popular pizza topping combination along with its rank and the number of times that combination has been ordered.
            var most = _pizza
                    .GroupBy(p => p.Combination)
                    .OrderByDescending(gr => gr.Count())
                    .Select(gr => gr.Select(p=> { p.HowOften = gr.Count();return p; } ).First())
                    .Take(n)
                    .Select((p,i) => { p.HowOftenRank = 1+i; return p; })
                    ;

            return most;
        }
    }
}

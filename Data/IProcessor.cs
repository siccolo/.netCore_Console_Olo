using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IProcessor
    {
        Task<IEnumerable<Models.Pizza>> PopulateDataAsync();
        //IEnumerable<Models.Pizza> PullTopNEntries(int n);
        IEnumerable<Models.Pizza> PullTopNEntries(int n);
    }
}

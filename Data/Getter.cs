using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using Microsoft.Extensions.Options;

namespace Data
{
    public interface IGetter
    {
        string DataUrl { get; }
        Task<Models.IDataResult> GetDataAsync();
    }

    public class Getter:IGetter
    {
        private readonly AppConfig.AppConfig _Config;
        private readonly IHttpClientFactory _HttpClientFatory;
        private readonly Logger.ILogger _Logger;

        public string DataUrl => _Config.DataUrl;

        public Getter(IOptions<AppConfig.AppConfig> config, IHttpClientFactory httpClientFactory, Logger.ILogger logger)
        {
            _Config = config.Value;
            _HttpClientFatory = httpClientFactory ?? throw new System.ArgumentNullException("httpClientFactory is missing"); ;
            _Logger = logger ?? throw new System.ArgumentNullException("logger is missing");
        }

        public async Task<Models.IDataResult> GetDataAsync()
        {
            try
            {
                var httpClient = _HttpClientFatory.CreateClient("getter");
                using (HttpResponseMessage response = await httpClient.GetAsync(this.DataUrl).ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string s = await response.Content.ReadAsStringAsync();
                        return new Models.DataResult(s);
                    }
                    else
                    {
                        var ex = new System.InvalidOperationException(response.ReasonPhrase + " \n" + response.RequestMessage.ToString());
                        return new Models.DataResult(ex);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Models.DataResult(ex);
            }
        }
    }
}

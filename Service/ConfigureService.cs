using System;
using System.Collections.Generic;
using System.Text;

using System.Net.Http;
using System.Net;
using System.Security.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;

//logging
using Microsoft.Extensions.Logging;
//config settings
using Microsoft.Extensions.Configuration;

namespace Service
{
    public static class ConfigureService
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection serviceCollection)
        {

            //  standard .netCore
            //serviceCollection.AddSingleton(new LoggerFactory().AddConsole().AddDebug());serviceCollection.AddLogging();

            //  httpclient factory
            serviceCollection.AddHttpClient("getter", client => { client.Timeout = TimeSpan.FromMinutes(1); client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0"); })
                            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
                            {
                                SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls
                            });

            var s = serviceCollection
                            //  dummy logger
                            .AddSingleton<Logger.ILogger, Logger.Logger>()
                            //  data downloader
                            .AddSingleton<Data.IGetter, Data.Getter>()
                            //  data processor
                            .AddSingleton<Data.IProcessor, Data.Processor>();

            return s;
        }

        public static IServiceCollection ConfigureAppSettings(this IServiceCollection serviceCollection)
        {

            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                                    .AddJsonFile("app-settings.json", false)
                                    .Build();

            serviceCollection.AddOptions();
            var s = serviceCollection.Configure<AppConfig.AppConfig>(configuration.GetSection("Configuration"));
            return s;
        }
    }
}

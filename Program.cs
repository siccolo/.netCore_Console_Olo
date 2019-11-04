using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Service;

namespace ConsoleOlo_netCore
{
    class Program
    {
        static void Main(string[] args)
        {
            DoWork().GetAwaiter().GetResult();
        }

        private static async Task DoWork()
        {
            /*
            Pizza Exercise
            One of our restaurant clients wants to know which pizza topping combinations are the most popular.
            Using a language in our technical stack, write an app or script that will download orders directly from http://files.olo.com/pizzas.json and output the top 20 most frequently ordered pizza topping combinations. 
            List the toppings for each popular pizza topping combination along with its rank and the number of times that combination has been ordered.
            For best results, focus on accuracy and brevity. Our estimate for this exercise is 30 minutes.
            */
            //  .NET Core, ASP.NET Core, TeamCity, GitHub, Octopus Deploy, and heavy use of OSS.
            //  ASP.NET, JavaScript, HTML5/CSS

            IServiceCollection services = new ServiceCollection();
            Startup startup = new Startup();
            startup.ConfigureServices(services);
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            var worker = serviceProvider.GetService<Data.IProcessor>();
            await worker.PopulateDataAsync();

            Console.WriteLine(" -- 20 top combinations: -- ");
            var top20 = worker.PullTopNEntries(20);
            foreach(var popularPizzaCombination in top20)
            {
                Console.WriteLine($"Rank {popularPizzaCombination.HowOftenRank.ToString()} Toppings [{popularPizzaCombination.Combination}] number of times {popularPizzaCombination.HowOften.ToString()}" );
            }            
        }        
    }
}

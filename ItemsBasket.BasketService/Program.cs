using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace ItemsBasket.BasketService
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((hostingContext, logging) => 
                {
                    logging.AddDebug();
                })
                .UseStartup<Startup>()
                .Build();
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

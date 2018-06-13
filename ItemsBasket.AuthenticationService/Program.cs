using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ItemsBasket.AuthenticationService
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
                .UseStartup<Startup>()
                .Build();
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Hosting = Microsoft.Extensions.Hosting;

namespace TwitchCommunity.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Hosting.Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

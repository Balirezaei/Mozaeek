using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace MozaeekCore.ReadConsistencyService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())  //location of the exe file
                        .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true);
                    IConfigurationRoot configuration = builder.Build();
                    var value = configuration.GetValue<string>("Urls");

                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls(value);
                });
    }
}
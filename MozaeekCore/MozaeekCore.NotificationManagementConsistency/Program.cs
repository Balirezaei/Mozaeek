using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MozaeekCore.NotificationManagementConsistency
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
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
}

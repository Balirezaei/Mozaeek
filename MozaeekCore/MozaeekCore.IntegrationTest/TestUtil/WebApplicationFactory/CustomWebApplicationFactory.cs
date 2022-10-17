using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Persistense.EF;
using MozaeekCore.RestAPI;
using MozaeekCore.RestAPI.Bootstrap;
using MozaeekCore.RestAPI.Utility;
using NSubstitute;

namespace MozaeekCore.IntegrationTest
{
    public class CustomWebApplicationFactory<TStartup> 
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        public CustomWebApplicationFactory()
        {
            
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
           
            var outputPath = Directory.GetCurrentDirectory();
            builder
                .UseEnvironment("IntegrationTest").ConfigureServices(services =>
            {

                var configuration = GetIConfigurationRoot(outputPath);

                services.AddScoped<CurrentUser>(provider =>
                {
                    var currentUser = new CurrentUser();
                    currentUser.UserId = Guid.NewGuid().ToString();
                    currentUser.UserName = "Integration_Test";
                    return currentUser;
                });

                services.AddDbContext<CoreDomainContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });
             
                //Mock 
                services.AddScoped<IMessagePublisher>(provider =>
                {
                    return Substitute.For<IMessagePublisher>();
                });
                
                services.AddFrameworkServices();
                services.AddCommandHandlerServices();
                services.AddQueryHandlerServices();
                services.AddAuthorizationServices(configuration);

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<CoreDomainContext>();
                    db.Database.EnsureCreated();

                  

                }
            });

            builder.ConfigureTestServices(services =>
            {
                services.AddMvc().AddApplicationPart(typeof(Startup).Assembly);
            });
        }

        public static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddUserSecrets("e3dfcccf-0cb3-423a-b302-e3e92e95c128")
                .AddEnvironmentVariables()
                .Build();
        }
    }


    public static class Utilities
    {
        #region snippet1
        public static void InitializeDbForTests(CoreDomainContext db)
        {
            // db.Messages.AddRange(CoreDomainContext());
            db.SaveChanges();
        }

       

        //public static List<Message> GetSeedingMessages()
        //{
        //    return new List<Message>()
        //    {
        //        new Message(){ Text = "TEST RECORD: You're standing on my scarf." },
        //        new Message(){ Text = "TEST RECORD: Would you like a jelly baby?" },
        //        new Message(){ Text = "TEST RECORD: To the rational mind, " +
        //                              "nothing is inexplicable; only unexplained." }
        //    };
        //}
        #endregion
    }


}
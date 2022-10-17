using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MozaeekUserProfile.ApplicationService.Contract;
using MozaeekUserProfile.Core.Core.CommandHandler;
using MozaeekUserProfile.Core.Core.MessagePublisher;
using MozaeekUserProfile.IntegrationTest.TestUtil.DbInit;
using MozaeekUserProfile.Persistense.EF;
using MozaeekUserProfile.RestAPI;
using MozaeekUserProfile.RestAPI.Bootstrap;
using MozaeekUserProfile.RestAPI.Extensions;

namespace MozaeekUserProfile.IntegrationTest.TestUtil.WebFactory
{
    public class WebFactoryInSqlServer<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        public WebFactoryInSqlServer()
        {
        }

        //public TestUserLogin CurrentUser { get; set; }

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

                    services.AddFrameworkServices();
                    var cnn = configuration.GetConnectionString("MozaeekUserProfileContext");

                    services.AddDbContext<MozaeekUserProfileContext>(options =>
                    {
                        //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                        options.UseSqlServer(configuration.GetConnectionString("MozaeekUserProfileContext"));
                    });
                   
                    //Mock 
                    //services.AddScoped<IMessagePublisher>(provider => { return Substitute.For<IMessagePublisher>(); });
                    //services.AddScoped<IErrorHandling>(provider => { return Substitute.For<IErrorHandling>(); });

                    services.AddDependencies();

                    //services.AddAuthentication("BmiSSO")
                    //    .AddScheme<BasicTestAuthenticationOptions, BasicAuthenticationHandler
                    //    >("BmiSSO", options => { });

                    services.AddAuthorization();
                    services.AddScoped<ITokenService, TokenService>();
                    services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
          

                    //services.AddScoped<CurrentUser>(provider =>
                    //{

                    //    var currentUser = new CurrentUser();
                    //    currentUser.UserId = this.CurrentUser.NationalCode;
                    //    currentUser.UserName = this.CurrentUser.UserName;
                    //    return currentUser;
                    //});

                    services.AddAuthorization(options =>
                    {
                        //                options.AddPolicy("Over18",policy => policy.Requirements.Add(new Over18Requirement()));
                    });

                    var sp = services.BuildServiceProvider();
                    using (var scope = sp.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var db = scopedServices.GetRequiredService<MozaeekUserProfileContext>();
                        db.Database.EnsureDeleted();
                        db.Database.EnsureCreated();
                        db.InitializeTestDatabaseInSQL();
                        //  db.Database.Migrate();
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

}
using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.IntegrationTest.TestUtil.DbInit;
using MozaeekCore.IntegrationTest.TestUtil.Fake;
using MozaeekCore.Persistense.EF;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.RestAPI;
using MozaeekCore.RestAPI.Bootstrap;
using NSubstitute;

namespace MozaeekCore.IntegrationTest
{
    public class WebFactoryInMongoDb<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
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

                    var readConfigurationSection = configuration.GetSection("ReadModelDatabaseSettings");

                    var readConfigCnn = configuration.GetSection("ReadModelDatabaseSettings:ConnectionString").Value;
                    var readConfigDb = configuration.GetSection("ReadModelDatabaseSettings:DatabaseName").Value;
                    services.AddScoped<ReadModelDatabaseSettings>(provider =>
                    {
                        return new ReadModelDatabaseSettings
                        {
                            ConnectionString = readConfigCnn,
                            DatabaseName = readConfigDb
                        };
                    });
                    //Mock 
                    services.AddScoped<IMessagePublisher>(provider =>
                        {
                            return new MessagePublisherIntoMongo();
                        });

                    services.AddScoped<SubjectQueryFactory>(provider =>
                    {
                        return new SubjectQueryFactory();
                    });



                    services.AddScoped<LabelQueryFactory>();
                    services.AddScoped<PointQueryFactory>(); 
                    services.AddScoped<RequestActQueryFactory>(); 
                    services.AddScoped<RequestTargetQueryFactory>();
                    services.AddScoped<AnnouncementQueryFactory>();
                    services.AddScoped<RequestQueryFactory>();

                    services.AddFrameworkServices();
                    services.AddCommandHandlerServices();
                    services.AddQueryHandlerServices();
                    services.AddAuthorizationServices(configuration);
                    services.AddScoped<MongoRepository, MongoRepository>();

                    services.AddScoped<IReadModelDatabaseSettings>(sp =>
                        new ReadModelDatabaseSettings
                        {
                            ConnectionString = readConfigCnn,
                            DatabaseName = readConfigDb
                        });

                    var sp = services.BuildServiceProvider();

                    using (var scope = sp.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var mongoDb = scopedServices.GetRequiredService<MongoRepository>();
                        var subjectQueryFactory = scopedServices.GetRequiredService<SubjectQueryFactory>();
                        var requestTargetQueryFactory = scopedServices.GetRequiredService<RequestTargetQueryFactory>();
                        var announcementQueryFactory = scopedServices.GetRequiredService<AnnouncementQueryFactory>();
                        var labelQueryFactory = scopedServices.GetRequiredService<LabelQueryFactory>();
                        var pointQueryFactory = scopedServices.GetRequiredService<PointQueryFactory>();
                        var requestActQueryFactory = scopedServices.GetRequiredService<RequestActQueryFactory>();
                        var requestQueryFactory = scopedServices.GetRequiredService<RequestQueryFactory>();
                        
                        var mongoInit = new MongoInitDb(mongoDb, subjectQueryFactory, requestTargetQueryFactory, announcementQueryFactory, labelQueryFactory, pointQueryFactory, requestActQueryFactory, requestQueryFactory);
                        mongoInit.DropDb();
                        mongoInit.InitDb();
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
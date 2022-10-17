using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MozaeekTechnicianProfile.ApiCall;
using MozaeekTechnicianProfile.ApplicationService.Services;
using MozaeekTechnicianProfile.Domain;
using MozaeekTechnicianProfile.Persistense.EF;
using MozaeekTechnicianProfile.Persistense.EF.Repository;
using MozaeekTechnicianProfile.TechnicianFinder.Job;
using MozaeekTechnicianProfile.TechnicianFinder.Service;
using MozaeekTechnicianProfile.UserQuestionProgress;

namespace MozaeekTechnicianProfile.TechnicianFinder.Config
{
    public class IocRegistration
    {
        private readonly IConfigurationRoot _configuration;

        public IocRegistration(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        public IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddMemoryCache();

            services.AddApiAggregatorHttpClient(_configuration);

            services.AddScoped<ServiceInformation>(provider =>
            {
                var serviceInfo = new ServiceInformation(_configuration["ServiceName"]);
                return serviceInfo;
            });
            var connectionString = this._configuration.GetConnectionString("MozaeekTechnicianProfileContext");
            services.AddDbContext<MozaeekTechnicianProfileContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IQuestionWorkflowService, QuestionWorkflowService>();
            services.AddScoped<IQuestionToProcessService, QuestionToProcessService>();
            services.AddScoped<ITechnicianRepository, TechnicianRepository>();
            services.AddScoped<IProperTechnicianFinderService, ProperTechnicianFinderService>();
            services.AddScoped<IProperTechnicianFinderServiceFacade, ProperTechnicianFinderServiceFacade>();
            


            services.AddScoped<ILogger, SerilogManager>();

            // services.AddScoped<IRSSManager, RSSManager>();
            //
            // services.AddScoped<IRssRetriveFacade, RssRetriveFacade>();

            services.AddScoped<OrchestratorConfiguration>();

            return services;
        }


    }
}
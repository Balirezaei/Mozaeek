using Microsoft.Extensions.DependencyInjection;
using MozaeekTechnicianProfile.ApplicationService.Services.TechnicianProfileServices;
using MozaeekTechnicianProfile.Core.Core;
using MozaeekTechnicianProfile.Core.Core.CommandBus;
using MozaeekTechnicianProfile.Core.Core.CommandHandler;
using MozaeekTechnicianProfile.Core.Core.QueryHandler;
using MozaeekTechnicianProfile.Persistense.EF;
using MozaeekTechnicianProfile.RestAPI.Managements;

namespace MozaeekTechnicianProfile.RestAPI.Bootstrap
{
    public static class FrameworkRegistration
    {
        public static IServiceCollection AddFrameworkServices(this IServiceCollection services)
        {

            services.AddSingleton<ICommandBus, CommandBus>();
            services.AddScoped<IQueryProcessor, QueryProcessor>();
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
            services.AddScoped<ITechnicianRegisterService, TechnicianRegisterService>();
            services.AddScoped<IErrorHandling, LogErrorHandle>();
            services.AddScoped<ILogManagement, DoLogManagement>();


            return services;
        }
    }
}
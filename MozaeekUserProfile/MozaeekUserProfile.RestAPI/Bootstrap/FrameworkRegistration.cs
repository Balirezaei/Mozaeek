using Microsoft.Extensions.DependencyInjection;
using MozaeekUserProfile.Core.Core;
using MozaeekUserProfile.Core.Core.CommandBus;
using MozaeekUserProfile.Core.Core.CommandHandler;
using MozaeekUserProfile.Core.Core.QueryHandler;
using MozaeekUserProfile.Persistense.EF;
using MozaeekUserProfile.RestAPI.Managements;

namespace MozaeekUserProfile.RestAPI.Bootstrap
{
    public static class FrameworkRegistration
    {
        public static IServiceCollection AddFrameworkServices(this IServiceCollection services)
        {

            services.AddSingleton<ICommandBus, CommandBus>();
            services.AddScoped<IQueryProcessor, QueryProcessor>();
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();

            services.AddScoped<IErrorHandling, LogErrorHandle>();
            services.AddScoped<ILogManagement, DoLogManagement>();


            return services;
        }
    }
}
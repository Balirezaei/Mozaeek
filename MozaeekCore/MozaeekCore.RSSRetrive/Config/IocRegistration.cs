using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MozaeekCore.RSSRetrive.Context;
using MozaeekCore.RSSRetrive.Job;
using MozaeekCore.RSSRetrive.Service;

namespace MozaeekCore.RSSRetrive.Config
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



            services.AddDbContext<FeedContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("FeedContext")), ServiceLifetime.Transient);



            services.AddScoped<ILogger, SerilogManager>();

            services.AddScoped<IRSSManager, RSSManager>();

            services.AddScoped<IRssRetriveFacade, RssRetriveFacade>();

            services.AddScoped<OrchestratorConfiguration>();
            return services;
        }


    }
}
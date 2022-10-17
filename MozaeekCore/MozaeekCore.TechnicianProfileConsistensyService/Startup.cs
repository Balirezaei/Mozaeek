using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.TechnicianProfileConsistensyService.Context;
using MozaeekCore.TechnicianProfileConsistensyService.Extension;
using MozaeekCore.TechnicianProfileConsistensyService.Service;

namespace MozaeekCore.TechnicianProfileConsistensyService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<TechnicianProfileContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TechnicianProfileContext")));
            services.AddScoped<ITechnicianUserQuestionService, TechnicianUserQuestionService>();
            services.AddScoped<IOutboxRepository, OutboxRepository>();
  
            services.AddDbContext<EventStoreContext>(options =>
                options.UseSqlServer(this.Configuration.GetConnectionString("EventStoreContext")));
            services.ConfigureMassTransit(Configuration);

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

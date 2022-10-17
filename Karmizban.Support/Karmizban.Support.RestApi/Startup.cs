using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Karmizban.Support.ApplicationService;
using Karmizban.Support.Domain;
using Karmizban.Support.EF.ContextContainer;
using Karmizban.Support.EF.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Karmizban.Support.RestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SupportContext>(options =>
                options.UseSqlServer(this.Configuration.GetConnectionString("SupportContext")));

            services.AddScoped<IUserSuggestedSupportService, UserSuggestedSupportService>();
            services.AddScoped<IUserRequestSupportService, UserRequestSupportService>();
            services.AddScoped<IUserRequestSupportRepository, UserRequestSupportRepository>();
            services.AddScoped<IUserSuggestedSupportRepository, UserSuggestedSupportRepository>();
            services.AddScoped<IUserRequestSupportAnswerRepository, UserRequestSupportAnswerRepository>();
            services.AddScoped<IUserReceivedMessageService, UserReceivedMessageService>();
            
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Karmizban.Support.RestApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Karmizban.Support.RestApi v1"));
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

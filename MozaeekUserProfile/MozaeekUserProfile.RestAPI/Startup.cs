using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MozaeekUserProfile.Domain.Contracts;
using MozaeekUserProfile.Persistense.EF;
using MozaeekUserProfile.RestAPI.Bootstrap;
using MozaeekUserProfile.RestAPI.Extensions;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using MozaeekUserProfile.OutBoxManagement;
using MozaeekUserProfile.Persistence.Mongo;

namespace MozaeekUserProfile.RestAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            CurrentEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        private IWebHostEnvironment CurrentEnvironment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (CurrentEnvironment.EnvironmentName == "IntegrationTest" || CurrentEnvironment.EnvironmentName == "IntegrationTestAuthorized")
            {
                services.AddControllers(config => {
                    config.Filters.Add(new ActionFilter());
                });
                return;
            }

            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod().AllowAnyHeader()
                    .WithExposedHeaders("Token-Expired");
            }));

            services.AddFrameworkServices();

            services.AddAuthorizationServices(this.Configuration);

            var connectionString = this.Configuration.GetConnectionString("MozaeekUserProfileContext");
            services.AddDbContext<MozaeekUserProfileContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddDbContext<EventStoreContext>(options =>
                options.UseSqlServer(this.Configuration.GetConnectionString("EventStoreContext")));


            services.Configure<ReadModelDatabaseSettings>(
                Configuration.GetSection(nameof(ReadModelDatabaseSettings)));

            services.AddScoped<IReadModelDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<ReadModelDatabaseSettings>>().Value);

            services.AddDependencies();

            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<CurrentUser>(provider =>
            {
                var claims = provider.GetService<IHttpContextAccessor>().HttpContext.User.Claims;
                var userIdClaim = claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                string userId = "";
                if (userIdClaim != null)
                {
                    userId = userIdClaim.Value;
                }
                var userNameClaim = claims.SingleOrDefault(c => c.Type == ClaimTypes.Name);
                string userName = "";
                if (userNameClaim != null)
                {
                    userName = userNameClaim.Value;
                }
                var currentUser = new CurrentUser();
                currentUser.UserId = userId;
                currentUser.UserName = userName;
                return currentUser;
            });
            services.ConfigureOptionSettings(Configuration);
            services.AddControllers(config =>
            {
                config.Filters.Add(new ActionFilter());
            });
       
            

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MozaeekUserProfile.RestAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName == "IntegrationTest" || env.EnvironmentName == "IntegrationTestAuthorized")
            {
                app.UseExceptionHandler("/error");
                app.UseRouting();
                if (env.EnvironmentName == "IntegrationTestAuthorized")
                {
                    app.UseAuthentication();
                    app.UseAuthorization();
                }
                app.UseEndpoints(endpoints => {
                    endpoints.MapControllers();
                });
                return;
            }
            app.UseCors("ApiCorsPolicy");
            //if (env.IsDevelopment())
            //{
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MozaeekUserProfile.RestAPI v1"));
            // }
            app.UseHttpsRedirection();
            app.UseExceptionHandler("/error");
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

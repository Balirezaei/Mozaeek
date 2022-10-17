using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MozaeekTechnicianProfile.Persistense.EF;
using MozaeekTechnicianProfile.RestAPI.Bootstrap;
using MozaeekTechnicianProfile.RestAPI.Extensions;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using MozaeekTechnicianProfile.ApplicationService.Contract;
using MozaeekTechnicianProfile.Infrastructure.Service;
using MozaeekTechnicianProfile.Persistence.Mongo;

namespace MozaeekTechnicianProfile.RestAPI
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
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod().AllowAnyHeader()
                    .WithExposedHeaders("Token-Expired");
            }));
     
            services.AddFrameworkServices();

         services.AddAuthorizationServices(this.Configuration);

            var connectionString = this.Configuration.GetConnectionString("MozaeekTechnicianProfileContext");
            services.AddDbContext<MozaeekTechnicianProfileContext>(options =>
                options.UseSqlServer(connectionString));

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

                var currentUser = new CurrentUser {UserId = userId, UserName = userName};
                return currentUser;
            });
            services.AddControllers(config =>
            {
                config.Filters.Add(new ActionFilter());
            });

            services.Configure<MicroserviceUrlsSettings>(Configuration.GetSection(MicroserviceUrlsSettings.Name));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MozaeekTechnicianProfile.RestAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("ApiCorsPolicy");
            //if (env.IsDevelopment())
            //{
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MozaeekTechnicianProfile.RestAPI v1"));
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

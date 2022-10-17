using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_Aggregator.ApplicationService.AggregationServices;
using Api_Aggregator.ApplicationService.AggregationServices.UserAnnoucementBasicInfo;
using Api_Aggregator.ApplicationService.AggregationServices.UserQuestion;
using Api_Aggregator.ApplicationService.MediationServices.MozaeekCore.BasicInfo;
using Api_Aggregator.ApplicationService.MediationServices.MozaeekCore.PriceInquery;
using Api_Aggregator.ApplicationService.MediationServices.UserProfile.AnnoucementUserDashboard;
using Api_Aggregator.ApplicationService.MediationServices.UserProfile.Characteristic;
using Api_Aggregator.ApplicationService.MediationServices.UserProfile.UserAnnouncement;
using Api_Aggregator.ApplicationService.MediationServices.UserProfile.UserQuestion;
using Api_Aggregator.Filters;
using Api_Aggregator.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Api_Aggregator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _rootConfiguration = (IConfigurationRoot)configuration;
        }

        public IConfiguration Configuration { get; }
        public IConfigurationRoot _rootConfiguration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(config =>
            {
                config.Filters.Add(new ActionFilter());
            });
            services.AddApiAggregatorHttpClient(_rootConfiguration);
            services.AddScoped<IAnnoucementUserDashboardBasicInfoAggregationService, AnnoucementUserDashboardBasicInfoAggregationService>();
            services.AddScoped<IBasicInfoMediationService, BasicInfoMediationService>();
            services.AddScoped<IAnnoucementUserDashboardMediationService, AnnoucementUserDashboardMediationService>();
            services.AddScoped<IUserRequestQuestionService, UserRequestQuestionService>();
            services.AddScoped<IPriceInqueryMediationService, PriceInqueryMediationService>();
            services.AddScoped<IUserQuestionMedationService, UserQuestionMedationService>();
            services.AddScoped<ICharachterisiticMediationService, CharachterisiticMediationService>();
            services.AddScoped<IUserProfileLabelService, UserProfileLabelService>();
            services.AddScoped<IUserProfileSubjectPriceService, UserProfileSubjectPriceService>();




        services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AggregationApi", Version = "v1" });
            });
            //services.AddSingleton
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
                app.UseDeveloperExceptionPage();
          //  }

            app.UseHttpsRedirection();
            app.UseExceptionHandler("/error");
            app.UseRouting();

            app.UseAuthorization();
       
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AggregationApi v1"));


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        
    }
}

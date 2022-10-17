using Microsoft.Extensions.DependencyInjection;
using MozaeekTechnicianProfile.ApplicationService.Contract;
using MozaeekTechnicianProfile.ApplicationService.Services;
using MozaeekTechnicianProfile.ApplicationService.Services.OtpServices;
using MozaeekTechnicianProfile.ApplicationService.Services.SenderServices;
using MozaeekTechnicianProfile.ApplicationService.Services.TechnicianProfileServices;
using MozaeekTechnicianProfile.Domain;
using MozaeekTechnicianProfile.Infrastructure.Service;
using MozaeekTechnicianProfile.Persistense.EF.Repository;
using MozaeekTechnicianProfile.RestAPI.ActionFilters;
using MozaeekTechnicianProfile.RestAPI.Services;

namespace MozaeekTechnicianProfile.RestAPI.Extensions
{
    public static class DependencyExtensions
    {
        public static void AddDependencies(this IServiceCollection services)
        {

            AddRepositories(services);
            AddServices(services);
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICodeSenderService, SmsCodeSenderService>();
      
         
            
            services.AddScoped<IOtpCodeGenerator, OtpCodeGenerator>();
            services.AddScoped<IOtpStarterService, OtpStarterService>();
            services.AddScoped<IOtpCodeRepository, OtpCodeSqlRepository>();
            services.AddScoped<ITechnicianRegisterService, TechnicianRegisterService>();
            services.AddScoped<ITechnicianAbsencePresenceService, TechnicianAbsencePresenceService>();
            services.AddScoped<CurrentUser>();
            services.AddHttpClient<IOtpSenderService, NotificationService>();
            services.AddScoped<IOtpVerifierService, OtpVerifierService>();
            services.AddScoped<CheckAuthActionFilter>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ITechnicianRepository, TechnicianRepository>();
            
        }
    }
}

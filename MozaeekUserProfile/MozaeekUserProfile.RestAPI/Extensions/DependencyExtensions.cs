using Microsoft.Extensions.DependencyInjection;
using MozaeekUserProfile.ApplicationService.Services;
using MozaeekUserProfile.ApplicationService.Services.BasicInfoQuery;
using MozaeekUserProfile.ApplicationService.Services.OtpCodeGeneratorServices;
using MozaeekUserProfile.ApplicationService.Services.OtpServices;
using MozaeekUserProfile.ApplicationService.Services.SenderServices;
using MozaeekUserProfile.ApplicationService.Services.UserAnnouncementServices;
using MozaeekUserProfile.ApplicationService.Services.UserDashboardService;
using MozaeekUserProfile.ApplicationService.Services.UserPoint;
using MozaeekUserProfile.ApplicationService.Services.UserProfileServices;
using MozaeekUserProfile.Core.Core.MessagePublisher;
using MozaeekUserProfile.Domain;
using MozaeekUserProfile.Domain.Contracts;
using MozaeekUserProfile.Infrastucture.Service.Services;
using MozaeekUserProfile.OutBoxManagement;
using MozaeekUserProfile.Persistence.Mongo;
using MozaeekUserProfile.Persistense.EF.Repository;
using MozaeekUserProfile.RestAPI.ActionFilters;

namespace MozaeekUserProfile.RestAPI.Extensions
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
            services.AddScoped<IOtpStarterService, OtpStarterService>();
            services.AddScoped<IOtpVerifierService, OtpVerifierService>();
            services.AddScoped<IOtpCodeGenerator, OtpCodeGenerator>();
            services.AddScoped<ICodeSenderService, SmsCodeSenderService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IUserDashboardService, UserDashboardService>();
            services.AddScoped<CurrentUser>();
            services.AddScoped<CheckAuthActionFilter>();
            services.AddHttpClient<IOtpSenderService, NotificationService>();
            services.AddScoped<IBasicInfoReadService, BasicInfoReadService>();
            services.AddScoped<IUserPointService, UserPointService>();
            services.AddScoped<IUserAnnouncementService, UserAnnouncementService>();
            services.AddScoped<IUserQuestionService, UserQuestionService>();
            services.AddScoped<IMessagePublisher, OutboxPublisher>();
            services.AddScoped<IOutboxRepository, OutboxRepository>();
            services.AddScoped<IUserQuestionQueryService, UserQuestionQueryService>();
            services.AddScoped<IUserWalletService, UserWalletService>();
            services.AddScoped<IUserProfileCharacteristicService, UserProfileCharacteristicService>();
            services.AddScoped<IQuestionWorkflowService, QuestionWorkflowService>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IOtpCodeRepository, OtpCodeSqlRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserDashboardRepository, UserDashboardRepository>();
            services.AddScoped<IMongoRepository, MongoRepository>();
            services.AddScoped<IUserPointRepository, UserPointRepository>();
            services.AddScoped<IUserQuestionRepository, UserQuestionRepository>();
            services.AddScoped<IUserWalletRepository, UserWalletRepository>();
            services.AddScoped<IUserProfileCharacteristicRepository, UserProfileCharacteristicRepository>();

        }
    }
}

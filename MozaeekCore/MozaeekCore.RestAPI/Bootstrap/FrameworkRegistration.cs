using System;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MozaeekCore.Common.Crypto;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandBus;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Identity;
using MozaeekCore.Domain.Pricing;
using MozaeekCore.LogManagement;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.EF;
using MozaeekCore.Persistense.EF.Repository;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.Persistense.MongoDb.Pricing;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace MozaeekCore.RestAPI.Bootstrap
{
    public static class FrameworkRegistration
    {
        public static IServiceCollection AddFrameworkServices(this IServiceCollection services)
        {
            services.AddScoped<ICommandBus, CommandBus>();
            services.AddScoped<IQueryProcessor, QueryProcessor>();
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
            services.AddScoped( typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IPreRequestRepository, PreRequestRepository>();
            services.AddScoped<ITechnicianRepository, TechnicianRepository>();
            services.AddScoped<IRequestRepository, RequestRepository>();
            services.AddScoped<IRequestTargetRepository, RequestTargetRepository>();
            services.AddScoped<INewsToProcessRepository, NewsToProcessRepository>();
            services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
            services.AddScoped<ISubjectPriceRepository, SubjectPriceRepository>();
            services.AddScoped<IRequestPriceRepository, RequestPriceRepository>();
            
            services.AddScoped<IUserRepository, UserRepository>();
        
       
            services.AddScoped<ILogManagement, DoLogManagement>();
            services.AddScoped(typeof(LoggingHandlerDecorator<,>));
            services.AddScoped(typeof(CatchErrorCommandHandlerDecorator<,>));
            services.AddScoped(typeof(AuthorizeCommandHandlerDecorator<,>));
            services.AddScoped(typeof(AuthorizeCommandAsyncHandlerDecorator<,>));
            
            services.AddScoped<IReadModelDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<ReadModelDatabaseSettings>>().Value);

            services.AddScoped<IMongoRepository, MongoRepository>();
            services.AddScoped<ILabelQueryService, LabelQueryService>();
            services.AddScoped<IPointQueryService, PointQueryService>();
            services.AddScoped<IRequestTargetQueryService, RequestTargetQueryService>();
            services.AddScoped<IRequestOrgQueryService, RequestOrgQueryService>();
            services.AddScoped<IRequestActQueryService, RequestActQueryService>();
            services.AddScoped<ISubjectQueryService, SubjectQueryService>();
            services.AddScoped<IRequestQueryService, RequestQueryService>();
            services.AddScoped<IAnnouncementQueryService, AnnouncementQueryService>();
            services.AddScoped<ITechnicianQueryService, TechnicianQueryService>();
            services.AddScoped<IRequestTargetQueryService, RequestTargetQueryService>();
            services.AddScoped<IBasicInfoQueryService, BasicInfoQueryService>();
            services.AddScoped<IRequestPriceQueryService, RequestPriceQueryService>();
            services.AddScoped<ISubjectPriceQueryService, SubjectPriceQueryService>();
            services.AddScoped<IPreRequestQueryService, PreRequestQueryService>();
            services.AddScoped<ISynonymQueryService, SynonymQueryService>();
            services.AddScoped<IDefiniteRequestOrgQueryService, DefiniteRequestOrgQueryService>();
            services.AddScoped<IOutboxRepository, OutboxRepository>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IFileRepository, FileRepository>();
            return services;
        }
    }
}
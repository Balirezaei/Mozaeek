using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandBus;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;
using MozaeekCore.LogManagement;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.EF;
using MozaeekCore.Persistense.EF.Repository;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.RestAPI.Bootstrap
{
    public static class FrameworkRegistration
    {
        public static IServiceCollection AddFrameworkServices(this IServiceCollection services)
        {
            //AutoMapper.Mapper.Initialize(cfg =>
            //    {
 
            //    }
            //);
            //services.AddAutoMapper();


            services.AddSingleton<ICommandBus, CommandBus>();
            services.AddScoped<IQueryProcessor, QueryProcessor>();
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
            services.AddScoped( typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnProcessedRequestRepository, UnProcessedRequestRepository>();
            services.AddScoped<ITechnicianRepository, TechnicianRepository>();
            services.AddScoped<IRequestRepository, RequestRepository>();
            services.AddScoped<IRequestTargetRepository, RequestTargetRepository>();
            services.AddScoped<INewsToProcessRepository, NewsToProcessRepository>();
            services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
            
            // services.AddScoped<IUnProcessedRequestQueryFacade, UnProcessedRequestQueryFacade>();
            services.AddScoped<IErrorHandling, LogErrorHandle>();
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

            services.AddScoped<IOutboxRepository, OutboxRepository>();

            return services;
        }
    }
}
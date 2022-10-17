using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MozaeekCore.ApplicationService.Query;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Facade.Query;

namespace MozaeekCore.RestAPI.Bootstrap
{
    public static class QueryHandlerRegistration
    {
        public static IServiceCollection AddQueryHandlerServices(this IServiceCollection services)
        {
            services.AddScoped<IUnProcessedRequestQueryFacade, UnProcessedRequestQueryFacade>();
            services.AddScoped<ITechnicianQueryFacade, TechnicianQueryFacade>();
            services.AddScoped<ILabelQueryFacade, LabelQueryFacade>();
            services.AddScoped<IPointQueryFacade, PointQueryFacade>();
            services.AddScoped<IRequestOrgQueryFacade, RequestOrgQueryFacade>();
            services.AddScoped<IRequestTargetQueryFacade, RequestTargetQueryFacade>();
            services.AddScoped<IRequestActQueryFacade, RequestActQueryFacade>();
            services.AddScoped<ISubjectQueryFacade, SubjectQueryFacade>();
            services.AddScoped<IRequestQueryFacade, RequestQueryFacade>();
            services.AddScoped<IRSSQueryFacade, RSSQueryFacade>();
            services.AddScoped<IAnnouncementQueryFacade, AnnouncementQueryFacade>();


            typeof(GetAllLabelParentQueryHandler)
                .Assembly
                .GetTypes()
                .Where(m => m.GetTypeInfo().ImplementedInterfaces.Where(z => z.IsGenericType)
                    .Any(z => z.GetGenericTypeDefinition() == typeof(IBaseAsyncQueryHandler<,>))).ToList().ForEach(assignedTypes =>
                {
                    try
                    {
                        var serviceType = assignedTypes.GetInterfaces().First(i => i.GetGenericTypeDefinition() == typeof(IBaseAsyncQueryHandler<,>));
                        services.AddScoped(serviceType, assignedTypes);
                    }
                    catch (System.Exception e)
                    {

                    }
                });

            return services;
        }
    }
}
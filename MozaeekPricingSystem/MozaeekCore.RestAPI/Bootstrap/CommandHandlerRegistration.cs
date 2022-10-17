using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MozaeekCore.ApplicationService;
using MozaeekCore.ApplicationService.Command;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.Commands;

namespace MozaeekCore.RestAPI.Bootstrap
{
    public static class CommandHandlerRegistration
    {
        public static IServiceCollection AddCommandHandlerServices(this IServiceCollection services)
        {
            typeof(CreateLabelValidator)
               .Assembly
               .GetTypes()
               .Where(m => m.GetTypeInfo().ImplementedInterfaces.Where(z => z.IsGenericType)
                   .Any(z => z.GetGenericTypeDefinition() == typeof(ICommandValidator<>))).ToList().ForEach(assignedTypes =>
               {
                   var serviceType = assignedTypes.GetInterfaces().First(i => i.GetGenericTypeDefinition() == typeof(ICommandValidator<>));

                   services.AddScoped(serviceType, assignedTypes);
               });


            typeof(CreateLabelCommandHandler)
                .Assembly
                .GetTypes()
                .Where(m => m.GetTypeInfo().ImplementedInterfaces.Where(z => z.IsGenericType)
                    .Any(z => z.GetGenericTypeDefinition() == typeof(IBaseAsyncCommandHandler<,>))).ToList().ForEach(assignedTypes =>
                {
                    try
                    {
                        var serviceType = assignedTypes.GetInterfaces().First(i => i.GetGenericTypeDefinition() == typeof(IBaseAsyncCommandHandler<,>));
                        services.AddScoped(serviceType, assignedTypes);
                    }
                    catch (System.Exception e)
                    {

                    }
                });


            // //if (ti.ImplementedInterfaces.Contains(typeof(T)))
            // //{
            // //    yield return (T)assembly.CreateInstance(ti.FullName);
            // //}
            // foreach (var type in result)
            // {
            //     services.AddScoped(typeof(ICommandValidator<>), type);
            //
            //     // CreateLabelValidator: ICommandValidator<CreateLabelCommand>
            // }
            //
            // System.Reflection.Assembly.GetExecutingAssembly()
            //     .GetTypes()
            //     .Where(item => item.GetInterfaces()
            //         .Where(i => i.IsGenericType)
            //         .Any(i => i.GetGenericTypeDefinition() == typeof(ICommandValidator<>)) && !item.IsAbstract && !item.IsInterface)
            //     .ToList()
            //     .ForEach(assignedTypes =>
            //     {
            //         var serviceType = assignedTypes.GetInterfaces().First(i => i.GetGenericTypeDefinition() == typeof(ICommandValidator<>));
            //         services.AddScoped(serviceType, assignedTypes);
            //     });


            // typeof(CreateUnProcessedRequestCommand).Assembly
            // services.Scan(scan =>
            //     scan.FromCallingAssembly()
            //         .AddClasses()
            //         .AsMatchingInterface());
            // services.TryAddScoped(scan => scan
            //     .FromAssemblies(assemblies)
            //     .AddClasses(classes => classes.AssignableTo<ILifecycle>(), publicOnly: false)
            //     .AsImplementedInterfaces()
            //     .WithTransientLifetime());

            return services;
        }
    }
}
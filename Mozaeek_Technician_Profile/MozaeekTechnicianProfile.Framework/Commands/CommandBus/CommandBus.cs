using System;
using System.Threading.Tasks;
using MozaeekTechnicianProfile.Core.Core.Base;
using MozaeekTechnicianProfile.Core.Core.CommandHandler;
using MozaeekTechnicianProfile.Core.Core.Commands;

namespace MozaeekTechnicianProfile.Core.Core.CommandBus
{
    public class CommandBus : ICommandBus
    {
        private readonly IServiceProvider services;

        public CommandBus(IServiceProvider services)
        {
            this.services = services;
        }
        // public TResult Dispatch<T, TResult>(T command) where T : Command
        // {
        //     IBaseCommandHandler<T, TResult> handler = null;
        //     handler = (IBaseCommandHandler<T, TResult>)services.GetService((typeof(AuthorizeCommandHandlerDecorator<T, TResult>)));
        //     var logManagement = (ILogManagement)services.GetService(typeof(ILogManagement));
        //     handler = new CatchErrorCommandHandlerDecorator<T, TResult>(new LoggingHandlerDecorator<T, TResult>(handler, logManagement));
        //     return handler.Handle(command);
        // }

        public Task<TResult> DispatchAsync<T, TResult>(T command) where T : Command
        {
            IBaseAsyncCommandHandler<T, TResult> handler = null;
            handler = (IBaseAsyncCommandHandler<T, TResult>)services.GetService((typeof(AuthorizeCommandAsyncHandlerDecorator<T, TResult>)));
            
            var logManagement = (ILogManagement)services.GetService(typeof(ILogManagement));

            handler = new CatchErrorCommandHandlerDecorator<T, TResult>(new AsyncLoggingHandlerDecorator<T, TResult>(handler, logManagement));

            var validator = (ICommandValidator<T>)services.GetService((typeof(ICommandValidator<T>)));

            handler = new ValidationCommandHandlerDecorator<T, TResult>(handler, validator);

            return handler.HandleAsync(command);
        }
    }
}
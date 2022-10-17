using System.Threading.Tasks;
using MozaeekUserProfile.Core.Core.Base;
using MozaeekUserProfile.Core.Core.Commands;

namespace MozaeekUserProfile.Core.Core.CommandHandler
{
    public class ValidationCommandHandlerDecorator<T, TResult> : IBaseAsyncCommandHandler<T, TResult> where T : Command
    {
        public ValidationCommandHandlerDecorator(IBaseAsyncCommandHandler<T, TResult> next, ICommandValidator<T> validator)
        {
            _validator = validator;
            _next = next;
        }


        public IBaseAsyncCommandHandler<T, TResult> _next { get; }
        private readonly ICommandValidator<T> _validator;

        public async Task<TResult> HandleAsync(T cmd)
        {
            if (_validator != null)
            {
                await _validator.ValidateAsync(cmd);
            }
            return await _next.HandleAsync(cmd);
        }
    }
}

//
// public class ValidationCommandHandlerDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult>
//     where TCommand : ICommand
// {
//     private readonly ICommandHandler<TCommand, TResult> _decoratedHandler;
//     private readonly ICommandValidator<TCommand> _validator;
//
//     public ValidationCommandHandlerDecorator(
//         ICommandHandler<TCommand, TResult> decoratedHandler,
//         ICommandValidator<TCommand> validator)
//     {
//         this._decoratedHandler = decoratedHandler;
//         this._validator = validator;
//     }
//
//     public async Task<TResult> HandleAsync(TCommand command)
//     {
//         await this._validator.ValidateAsync(command);
//         return await this._decoratedHandler.HandleAsync(command);
//
//
//     }
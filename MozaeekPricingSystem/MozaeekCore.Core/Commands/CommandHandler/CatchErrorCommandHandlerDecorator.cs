using System;
using System.Threading.Tasks;
using MozaeekCore.Core.Base;

namespace MozaeekCore.Core.CommandHandler
{
    public class CatchErrorCommandHandlerDecorator<T, TResult> : IBaseCommandHandler<T, TResult>,  IBaseAsyncCommandHandler<T, TResult> where T : Command
    {
        public CatchErrorCommandHandlerDecorator(IBaseCommandHandler<T, TResult> next)
        {

            _next = next;
        }
        public CatchErrorCommandHandlerDecorator(IBaseAsyncCommandHandler<T, TResult> next)
        {

            _nextAsync = next;
        }

        public IBaseCommandHandler<T, TResult> _next { get; }
        public IBaseAsyncCommandHandler<T, TResult> _nextAsync { get; }

        public TResult Handle(T cmd)
        {
            try
            {
                return _next.Handle(cmd);
            }
            catch (Exception e)
            {
                //Log the Eddor 
                Console.WriteLine(e);
                throw e;
            }
        }

        public Task<TResult> HandleAsync(T cmd)
        {
            try
            {
                return _nextAsync.HandleAsync(cmd);
            }
            catch (Exception e)
            {
                //Log the Eddor 
                Console.WriteLine(e);
                throw e;
            }
        }
    }

}
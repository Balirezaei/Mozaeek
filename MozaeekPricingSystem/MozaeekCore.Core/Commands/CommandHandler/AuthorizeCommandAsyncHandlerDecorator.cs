using MozaeekCore.Core.Base;
using System.Threading.Tasks;

namespace MozaeekCore.Core.CommandHandler
{
    public class AuthorizeCommandAsyncHandlerDecorator<T, TResult> :  IBaseAsyncCommandHandler<T, TResult> where T : Command
    {
        public AuthorizeCommandAsyncHandlerDecorator(IBaseAsyncCommandHandler<T, TResult> next)
        {
            _next = next;
        }
      
        
        public IBaseAsyncCommandHandler<T, TResult> _next { get; }

     
        public Task<TResult> HandleAsync(T cmd)
        {
            return _next.HandleAsync(cmd);
        }
    }
}
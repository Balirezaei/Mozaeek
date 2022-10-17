using MozaeekUserProfile.Core.Core.Base;
using System.Threading.Tasks;

namespace MozaeekUserProfile.Core.Core.CommandHandler
{
    public interface IBaseCommandHandler<T, TResult> where T : Command
    {
        TResult Handle(T cmd);        
    }
}
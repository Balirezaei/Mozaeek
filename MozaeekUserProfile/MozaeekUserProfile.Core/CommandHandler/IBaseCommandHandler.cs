using MozaeekUserProfile.Core.Base;
using System.Threading.Tasks;

namespace MozaeekUserProfile.Core.CommandHandler
{
    public interface IBaseCommandHandler<T, TResult> where T : Command
    {
        TResult Handle(T cmd);        
    }
}
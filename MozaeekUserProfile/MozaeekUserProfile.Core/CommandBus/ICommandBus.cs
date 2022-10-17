using MozaeekUserProfile.Core.Base;
using System.Threading.Tasks;

namespace MozaeekUserProfile.Core.CommandBus
{
    public interface ICommandBus
    {
        Task<TResult> DispatchAsync<T, TResult>(T command) where T : Command;
    }
}
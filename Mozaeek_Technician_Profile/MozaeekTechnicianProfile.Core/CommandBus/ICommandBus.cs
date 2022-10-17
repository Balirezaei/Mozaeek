using MozaeekTechnicianProfile.Core.Base;
using System.Threading.Tasks;

namespace MozaeekTechnicianProfile.Core.CommandBus
{
    public interface ICommandBus
    {
        Task<TResult> DispatchAsync<T, TResult>(T command) where T : Command;
    }
}
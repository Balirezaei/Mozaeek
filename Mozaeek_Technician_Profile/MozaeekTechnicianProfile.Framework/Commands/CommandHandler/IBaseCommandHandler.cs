using MozaeekTechnicianProfile.Core.Core.Base;
using System.Threading.Tasks;

namespace MozaeekTechnicianProfile.Core.Core.CommandHandler
{
    public interface IBaseCommandHandler<T, TResult> where T : Command
    {
        TResult Handle(T cmd);        
    }
}
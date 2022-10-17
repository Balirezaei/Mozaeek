using System.Threading.Tasks;
using MozaeekTechnicianProfile.Core.Core.Base;

namespace MozaeekTechnicianProfile.Core.Core.Commands
{
    public interface ICommandValidator<T> where T : Command
    {
        ValueTask ValidateAsync(T command);
    }
}
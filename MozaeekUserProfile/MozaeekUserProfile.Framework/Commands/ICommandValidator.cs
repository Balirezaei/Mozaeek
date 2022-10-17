using System.Threading.Tasks;
using MozaeekUserProfile.Core.Core.Base;

namespace MozaeekUserProfile.Core.Core.Commands
{
    public interface ICommandValidator<T> where T : Command
    {
        ValueTask ValidateAsync(T command);
    }
}
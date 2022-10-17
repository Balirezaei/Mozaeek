using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Common.ExtensionMethod;
using MozaeekCore.Core.Commands;
using MozaeekCore.Exceptions;

namespace MozaeekCore.ApplicationService.Command
{
    public class CreateRequestTargetValidator : ICommandValidator<CreateRequestTargetCommand>
    {
        public  ValueTask ValidateAsync(CreateRequestTargetCommand command)
        {
            if (command.Title.IsNullOrEmpty())
            {
                throw new UserFriendlyException("عنوان کار نشان نمی تواند خالی باشد.");
            }
            return default;
        }
    }
}
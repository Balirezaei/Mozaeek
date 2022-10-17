using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Common.ExtentionMethod;
using MozaeekCore.Core.Commands;
using MozaeekCore.Exceptions;

namespace MozaeekCore.ApplicationService.Command
{
    public class CreatePointValidator : ICommandValidator<CreatePointCommand>
    {
        public  ValueTask ValidateAsync(CreatePointCommand command)
        {
            if (command.Title.IsNullOrEmpty())
            {
                throw new UserFriendlyException("عنوان نقطه نمی تواند خالی باشد.");
            }
            return default;
        }
    }
}
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Common.ExtentionMethod;
using MozaeekCore.Core.Commands;
using MozaeekCore.Exceptions;

namespace MozaeekCore.ApplicationService.Command
{
    public class CreateRequestOrgValidator : ICommandValidator<CreateRequestOrgCommand>
    {
        public ValueTask ValidateAsync(CreateRequestOrgCommand command)
        {
            if (command.Title.IsNullOrEmpty())
            {
                throw new UserFriendlyException("عنوان خواستگاه نمی تواند خالی باشد.");
            }
            return default;
        }
    }
}
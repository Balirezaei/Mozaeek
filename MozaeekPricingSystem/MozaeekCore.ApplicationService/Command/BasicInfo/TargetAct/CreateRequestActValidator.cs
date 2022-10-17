using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Common.ExtentionMethod;
using MozaeekCore.Core.Commands;
using MozaeekCore.Exceptions;

namespace MozaeekCore.ApplicationService.Command
{
    public class CreateRequestActValidator : ICommandValidator<CreateRequestActCommand>
    {
        public  ValueTask ValidateAsync(CreateRequestActCommand actCommand)
        {
            if (actCommand.Title.IsNullOrEmpty())
            {
                throw new UserFriendlyException("عنوان کار واژه نمی تواند خالی باشد.");
            }
            return default;
        }
    }
}
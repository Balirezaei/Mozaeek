using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Common.ExtensionMethod;
using MozaeekCore.Core.Commands;
using MozaeekCore.Exceptions;

namespace MozaeekCore.ApplicationService.Command.RequestPrice
{
    public class CreateRequestPriceValidator : ICommandValidator<CreateRequestPriceCommand>
    {
        public ValueTask ValidateAsync(CreateRequestPriceCommand command)
        {
            if (command.Title.IsNullOrEmpty())
            {
                throw new UserFriendlyException("عنوان نمی تواند خالی باشد.");
            }
            if (command.RequestIds==null || !command.RequestIds.Any())
            {
                throw new UserFriendlyException("انتخاب حداقل یک خواست الزامیست.");
            }
            return default;
        }
    }
}
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Common.ExtensionMethod;
using MozaeekCore.Core.Commands;
using MozaeekCore.Exceptions;

namespace MozaeekCore.ApplicationService.Command.SubjectPrice
{
    public class UpdateSubjectPriceValidator : ICommandValidator<UpdateSubjectPriceCommand>
    {
        public ValueTask ValidateAsync(UpdateSubjectPriceCommand command)
        {
            if (command.Title.IsNullOrEmpty())
            {
                throw new UserFriendlyException("عنوان نمی تواند خالی باشد.");
            }
            if (command.SubjectIds == null || !command.SubjectIds.Any())
            {
                throw new UserFriendlyException("انتخاب حداقل یک خواست الزامیست.");
            }
            return default;
        }
    }
}
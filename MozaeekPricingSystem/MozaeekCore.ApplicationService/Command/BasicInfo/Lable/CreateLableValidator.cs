using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Common.ExtentionMethod;
using MozaeekCore.Core.Commands;
using MozaeekCore.Exceptions;
using System.Threading.Tasks;

namespace MozaeekCore.ApplicationService.Command
{
    public class CreateLabelValidator : ICommandValidator<CreateLabelCommand>
    {
        public ValueTask ValidateAsync(CreateLabelCommand command)
        {
            if (command.Title.IsNullOrEmpty())
            {
                throw new UserFriendlyException("عنوان برچسب نمی تواند خالی باشد.");
            }
            return default;
        }
    }
}
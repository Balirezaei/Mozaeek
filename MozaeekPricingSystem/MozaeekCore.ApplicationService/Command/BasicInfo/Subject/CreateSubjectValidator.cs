using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Common.ExtentionMethod;
using MozaeekCore.Core.Commands;
using MozaeekCore.Exceptions;

namespace MozaeekCore.ApplicationService.Command
{
    public class CreateSubjectValidator : ICommandValidator<CreateSubjectCommand>
    {
        public  ValueTask ValidateAsync(CreateSubjectCommand command)
        {
            if (command.Title.IsNullOrEmpty())
            {
                throw new UserFriendlyException("عنوان برچسب نمی تواند خالی باشد.");
            }
            return default;
        }
    }
}
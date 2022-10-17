using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Common.ExtensionMethod;
using MozaeekCore.Core.Commands;
using MozaeekCore.Exceptions;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract.Announcement;

namespace MozaeekCore.ApplicationService.Command
{
    public class CreateAnnouncementValidator : ICommandValidator<CreateAnnouncementCommand>
    {
        public ValueTask ValidateAsync(CreateAnnouncementCommand command)
        {
            if (command.Title.IsNullOrEmpty())
            {
                throw new UserFriendlyException("عنوان نمی تواند خالی باشد.");
            }
            return default;
        }
    }

}
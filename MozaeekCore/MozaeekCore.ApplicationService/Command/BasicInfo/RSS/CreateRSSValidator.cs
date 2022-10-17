using System;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Common.ExtensionMethod;
using MozaeekCore.Core.Commands;
using MozaeekCore.Exceptions;

namespace MozaeekCore.ApplicationService.Command
{
    public class CreateRSSValidator : ICommandValidator<CreateRSSCommand>
    {
        public async ValueTask ValidateAsync(CreateRSSCommand command)
        {
            if (command.Url.IsNullOrEmpty())
            {
                throw new UserFriendlyException("آدرس خبرخوان نمی تواند خالی باشد.");
            }
        }
    }
}
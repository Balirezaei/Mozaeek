using System;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Common.ExtensionMethod;
using MozaeekCore.Core.Commands;
using MozaeekCore.Exceptions;

namespace MozaeekCore.ApplicationService.Command
{
    public class CreatePropertyValidator : ICommandValidator<CreatePropertyCommand>
    {
        public async ValueTask ValidateAsync(CreatePropertyCommand command)
        {
            if (command.Title.IsNullOrEmpty())
            {
                throw new UserFriendlyException("عنوان نمی تواند خالی باشد.");
            }
        }
    }
}
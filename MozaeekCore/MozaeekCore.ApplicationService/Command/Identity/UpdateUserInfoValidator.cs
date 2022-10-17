using MozaeekCore.ApplicationService.Contract.Identity;
using MozaeekCore.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekCore.ApplicationService.Command.Identity
{
    public class UpdateUserInfoValidator : ICommandValidator<UpdateUserInfoCommand>
    {
        public ValueTask ValidateAsync(UpdateUserInfoCommand command)
        {
            return default;
        }
    }
}

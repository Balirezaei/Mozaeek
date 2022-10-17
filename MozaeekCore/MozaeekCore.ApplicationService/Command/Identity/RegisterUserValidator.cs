using MozaeekCore.ApplicationService.Contract.Identity;
using MozaeekCore.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekCore.ApplicationService.Command.Identity
{
    public class RegisterUserValidator : ICommandValidator<RegisterUserCommand>
    {
        public ValueTask ValidateAsync(RegisterUserCommand command)
        {
            
            return default;
        }
    }
}

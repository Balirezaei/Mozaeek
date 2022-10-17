using MozaeekCore.ApplicationService.Contract.Identity;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekCore.ApplicationService.Command.Identity
{
    public class UnAssignRoleToUserCommandHandler : IBaseAsyncCommandHandler<UnAssignRoleToUserCommand, UnAssignRoleToUserCommandResult>
    {
        private readonly IUserRepository userRepository;

        public UnAssignRoleToUserCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<UnAssignRoleToUserCommandResult> HandleAsync(UnAssignRoleToUserCommand cmd)
        {
            await userRepository.RemoveRole(cmd.UserId, cmd.Role);
            return new UnAssignRoleToUserCommandResult() { Result = true };
        }
    }
}

using MozaeekCore.ApplicationService.Contract.Identity;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekCore.ApplicationService.Command.Identity
{
    public class AssignRoleToUserCommandHandler : IBaseAsyncCommandHandler<AssignRoleToUserCommand, AssignRoleToUserCommandResult>
    {
        private readonly IUserRepository userRepository;
       

        public AssignRoleToUserCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
       
        }
        public async  Task<AssignRoleToUserCommandResult> HandleAsync(AssignRoleToUserCommand cmd)
        {
            await userRepository.AddRole(cmd.UserId, cmd.Role);
            return new AssignRoleToUserCommandResult() {Result=true };
        }
    }
}

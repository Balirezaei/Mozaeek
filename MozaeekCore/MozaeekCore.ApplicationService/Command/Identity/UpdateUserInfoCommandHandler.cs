using MozaeekCore.ApplicationService.Contract.Identity;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekCore.ApplicationService.Command.Identity
{
    public class UpdateUserInfoCommandHandler : IBaseAsyncCommandHandler<UpdateUserInfoCommand, UpdateUserInfoCommandResult>
    {
        private readonly IUserRepository userRepository;        

        public UpdateUserInfoCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;            
        }
        public async Task<UpdateUserInfoCommandResult> HandleAsync(UpdateUserInfoCommand cmd)
        {
            await userRepository.UpdateUser(new User()
            {
                EMail = cmd.EMail,
                FirstName = cmd.FirstName,
                LastName = cmd.LastName,
                NationalCode = cmd.NationalCode                
            },cmd.UserId, cmd.Roles.Select(m => new UserRole(m,cmd.UserId)).ToList());            

            return new UpdateUserInfoCommandResult() { Result = true};
        }
    }
}

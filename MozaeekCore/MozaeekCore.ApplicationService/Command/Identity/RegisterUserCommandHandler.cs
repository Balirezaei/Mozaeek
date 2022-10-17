using MozaeekCore.ApplicationService.Contract.Identity;
using MozaeekCore.Common.Crypto;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Domain.Identity;
using MozaeekCore.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekCore.ApplicationService.Command.Identity
{
    public class RegisterUserCommandHandler : IBaseAsyncCommandHandler<RegisterUserCommand, RegisterUserCommandResult>
    {
        private readonly IUserRepository userRepository;        
        private readonly IUnitOfWork unitOfWork;

        public RegisterUserCommandHandler(IUserRepository userRepository,                                          
                                          IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;            
            this.unitOfWork = unitOfWork;
        }
        public async Task<RegisterUserCommandResult> HandleAsync(RegisterUserCommand cmd)
        {
            var existUserName =await userRepository.CheckExistUser(cmd.UserName);

            if (existUserName)
            {
                throw new UserFriendlyException("user name cannot be duplicated!");
            }

            var newUser = new User()
            {
                EMail = cmd.EMail,
                FirstName=cmd.FirstName,
                LastName=cmd.LastName,
                NationalCode=cmd.NationalCode,
                UserName = cmd.UserName,           
                Password=cmd.Password,
                UserRoles =  cmd.Roles.Select(m=>new UserRole(m)).ToList()                
            };

            userRepository.AddNew(newUser);
            await unitOfWork.CommitAsync();

            return new RegisterUserCommandResult()
            {
                UserId = newUser.Id
            };
        }
    }
}

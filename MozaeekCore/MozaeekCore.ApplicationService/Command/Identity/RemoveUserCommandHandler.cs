using MozaeekCore.ApplicationService.Contract.Identity;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MozaeekCore.ApplicationService.Command.Identity
{
    public class RemoveUserCommandHandler : IBaseAsyncCommandHandler<RemoveUserCommand, RemoveUserCommandResult>
    {
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;

        public RemoveUserCommandHandler(IUserRepository userRepository,
                                          IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<RemoveUserCommandResult> HandleAsync(RemoveUserCommand cmd)
        {
            var user =  await userRepository.GetAll().SingleOrDefaultAsync(x=>x.Id==cmd.UserId);
            if (user == null)
            {
                throw new KeyNotFoundException("User Not Found");
            }
            await userRepository.RemoveRoles(user.Id);
            userRepository.Delete(user);
            await unitOfWork.CommitAsync();
            return new RemoveUserCommandResult() { Result = true };
        }
    }
}

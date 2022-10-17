using MozaeekCore.ApplicationService.Contract.Identity;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Identity;
using MozaeekCore.QueryModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using MozaeekCore.Mapper;
using Microsoft.EntityFrameworkCore;

namespace MozaeekCore.ApplicationService.Query.BasicInfo.Identity
{
    public class GetAllUserQueryHandler : IBaseAsyncQueryHandler<PagingContract, List<UserDto>>
    {
        private readonly IGenericRepository<User> userRepository;

        public GetAllUserQueryHandler(IGenericRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        public  Task<List<UserDto>> HandleAsync(PagingContract queryParam)
        {
            return userRepository.GetAll()
                                 .Skip((queryParam.PageNumber - 1) * queryParam.PageSize)
                                 .Take(queryParam.PageSize)
                                 .Include(x=>x.UserRoles)
                                 .Select(x => x.GetUserDto())
                                 .ToListAsync();
                                                     
        }
    }
}

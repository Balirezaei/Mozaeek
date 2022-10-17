using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Identity;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Identity;
using MozaeekCore.Exceptions;
using MozaeekCore.Mapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MozaeekCore.ApplicationService.Query.BasicInfo.Identity
{
    public class GetUserByIdQueryHandler : IBaseAsyncQueryHandler<FindByKey, UserDto>
    {
        private readonly IGenericRepository<User> _repository;

        public GetUserByIdQueryHandler(IGenericRepository<User> repository)
        {
            _repository = repository;
        }
        public async Task<UserDto> HandleAsync(FindByKey query)
        {
            var res = await _repository.GetAll()
                                       .Include(x=>x.UserRoles)
                                       .SingleOrDefaultAsync(x=>x.Id==query.Id);
            if (res == null)
            {
                throw new UserFriendlyException("اطلاعات یافت نشد");
            }
            return res.GetUserDto();
        }
    }
}

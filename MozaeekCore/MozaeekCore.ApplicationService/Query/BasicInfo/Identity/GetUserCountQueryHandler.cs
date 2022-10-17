using Microsoft.EntityFrameworkCore;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Identity;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekCore.ApplicationService.Query.BasicInfo.Identity
{
    public class GetUserCountQueryHandler : IBaseAsyncQueryHandler<Nothing, UserTotalCount>
    {
        private readonly IGenericRepository<User> _repository;

        public GetUserCountQueryHandler(IGenericRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<UserTotalCount> HandleAsync(Nothing query)
        {
            long count = await _repository.GetAll().CountAsync();
            return new UserTotalCount(count);
        }
    }
}

using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Identity;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.QueryModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MozaeekCore.Facade.Query.Identity
{
    public class UserQueryFacade : IUserQueryFacade
    {
        private readonly IQueryProcessor queryProcessor;

        public UserQueryFacade(IQueryProcessor queryProcessor)
        {
            this.queryProcessor = queryProcessor;
        }
        public Task<UserDto> GetUserById(long id)
        {
            return queryProcessor.ProcessAsync<FindByKey, UserDto>(new FindByKey(id));
        }

        public async Task<PagedListResult<UserDto>> GetUserDtos(PagingContract pagingContract)
        {
            var result = await queryProcessor.ProcessAsync<PagingContract, List<UserDto>>(pagingContract);
            var count = await queryProcessor.ProcessAsync<Nothing, UserTotalCount>(new Nothing());
            return new PagedListResult<UserDto>()
            {
                List = result,
                PageNumber = pagingContract.PageNumber,
                PageSize = pagingContract.PageSize,
                TotalCount = count.Count
            };
        }
    }
}

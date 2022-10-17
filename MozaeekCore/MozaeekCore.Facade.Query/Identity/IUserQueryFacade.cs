using MozaeekCore.ApplicationService.Contract.Identity;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.QueryModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekCore.Facade.Query.Identity
{
    public interface IUserQueryFacade
    {
        Task<UserDto> GetUserById(long id);
        Task<PagedListResult<UserDto>> GetUserDtos(PagingContract pagingContract);
    }
}

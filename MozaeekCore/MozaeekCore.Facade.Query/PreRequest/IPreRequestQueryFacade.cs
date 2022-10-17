using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.QueryModel;

namespace MozaeekCore.Facade.Query
{
    public interface IPreRequestQueryFacade
    {
        Task<PreRequestDto> GetPreRequestById(long id);
        Task<PagedListResult<PreRequestGrid>> GetAllPreRequests(PagingContract pagingContract);
    }

}
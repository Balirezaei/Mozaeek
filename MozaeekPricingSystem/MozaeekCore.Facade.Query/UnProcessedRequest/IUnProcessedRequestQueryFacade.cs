using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;

namespace MozaeekCore.Facade.Query
{
    public interface IUnProcessedRequestQueryFacade
    {
        Task<UnProcessedRequestDto> GetProcessedRequestById(int id);
    }
}
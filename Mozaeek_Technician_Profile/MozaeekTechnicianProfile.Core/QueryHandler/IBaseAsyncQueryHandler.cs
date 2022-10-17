using System.Threading.Tasks;

namespace MozaeekTechnicianProfile.Core.QueryHandler
{
    public interface IBaseAsyncQueryHandler<TQuery, TResult> //where TQuery : Query
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}
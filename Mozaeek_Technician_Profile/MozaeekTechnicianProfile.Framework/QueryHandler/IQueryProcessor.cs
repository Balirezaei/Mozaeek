using System.Threading.Tasks;

namespace MozaeekTechnicianProfile.Core.Core.QueryHandler
{
    public interface IQueryProcessor
    {
        TResult Process<TQuery, TResult>(TQuery query);
        Task<TResult> ProcessAsync<TQuery, TResult>(TQuery query);
        bool Check<TQuery, TResult>(TQuery query);
    }
}
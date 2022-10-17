using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;
using MozaeekCore.Mapper;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetRequestTargetByIdQueryHandler : IBaseAsyncQueryHandler<FindByKey, RequestTargetDto>
    {
        private readonly IRequestTargetRepository _repository;

        public GetRequestTargetByIdQueryHandler(IRequestTargetRepository repository)
        {
            _repository = repository;
        }
        public async Task<RequestTargetDto> HandleAsync(FindByKey query)
        {
            var res = await _repository.Find(query.Id);
            return res.GetRequestTargetDto();
        }
    }
}
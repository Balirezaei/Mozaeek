using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;
using MozaeekCore.Mapper;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetUnProcessedRequestByIdQueryHandler : IBaseAsyncQueryHandler<FindByKey, UnProcessedRequestDto>
    {
        private readonly IUnProcessedRequestRepository _unProcessedRequestRepository;

        public GetUnProcessedRequestByIdQueryHandler(IUnProcessedRequestRepository unProcessedRequestRepository)
        {
            _unProcessedRequestRepository = unProcessedRequestRepository;
        }

        public async Task<UnProcessedRequestDto> HandleAsync(FindByKey query)
        {
            var res =await _unProcessedRequestRepository.Find(query.Id);
            return res.GetUnProcessedRequestDto();
        }
    }
}
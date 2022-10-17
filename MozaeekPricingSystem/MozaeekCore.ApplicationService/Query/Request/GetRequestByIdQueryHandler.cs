using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;
using MozaeekCore.Mapper;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetRequestByIdQueryHandler : IBaseAsyncQueryHandler<FindByKey, RequestDto>
    {
        private readonly IRequestRepository _requestRepository;

        public GetRequestByIdQueryHandler(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public async Task<RequestDto> HandleAsync(FindByKey query)
        {
            var request = await _requestRepository.Find(query.Id);
            return request.GetRequestDto();
        }
    }
}
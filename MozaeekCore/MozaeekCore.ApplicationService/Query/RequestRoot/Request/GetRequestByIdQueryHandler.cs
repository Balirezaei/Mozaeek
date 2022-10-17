using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;
using MozaeekCore.Mapper;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetRequestByIdQueryHandler : IBaseAsyncQueryHandler<FindByKey, RequestDto>
    {
        private readonly IRequestQueryService _requestQueryService;

        public GetRequestByIdQueryHandler(IRequestQueryService requestQueryService)
        {
            _requestQueryService = requestQueryService;
        }

        public async Task<RequestDto> HandleAsync(FindByKey query)
        {
            var request = await _requestQueryService.Get(query.Id);
            return request.GetRequestDto();
        }
    }
}
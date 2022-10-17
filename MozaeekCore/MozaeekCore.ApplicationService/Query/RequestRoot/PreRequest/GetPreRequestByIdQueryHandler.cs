using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;
using MozaeekCore.Mapper;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetPreRequestByIdQueryHandler : IBaseAsyncQueryHandler<FindByKey, PreRequestDto>
    {
        private readonly IPreRequestRepository _preRequestRepository;

        public GetPreRequestByIdQueryHandler(IPreRequestRepository preRequestRepository)
        {
            _preRequestRepository = preRequestRepository;
        }

        public async Task<PreRequestDto> HandleAsync(FindByKey query)
        {
            var res =await _preRequestRepository.Find(query.Id);
            return res.GetPreRequestDto();
        }
    }
}
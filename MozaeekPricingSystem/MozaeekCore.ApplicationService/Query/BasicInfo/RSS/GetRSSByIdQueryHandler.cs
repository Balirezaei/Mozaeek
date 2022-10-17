using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;
using MozaeekCore.Exceptions;
using MozaeekCore.Mapper;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetRSSByIdQueryHandler : IBaseAsyncQueryHandler<FindByKey, RSSDto>
    {
        private readonly IGenericRepository<RSS> _repository;

        public GetRSSByIdQueryHandler(IGenericRepository<RSS> repository)
        {
            _repository = repository;
        }
        public async Task<RSSDto> HandleAsync(FindByKey query)
        {
            var res = await _repository.Find(query.Id);
            if (res == null)
            {
                throw new UserFriendlyException("اطلاعات یافت نشد");
            }
            return res.GetRSSDto();
        }
    }
}
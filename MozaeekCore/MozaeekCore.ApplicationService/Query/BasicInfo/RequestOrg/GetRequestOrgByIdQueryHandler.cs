using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;
using MozaeekCore.Exceptions;
using MozaeekCore.Mapper;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetRequestOrgByIdQueryHandler : IBaseAsyncQueryHandler<FindByKey, RequestOrgDto>
    {
        private readonly IGenericRepository<RequestOrg> _repository;

        public GetRequestOrgByIdQueryHandler(IGenericRepository<RequestOrg> repository)
        {
            _repository = repository;
        }
        public async Task<RequestOrgDto> HandleAsync(FindByKey query)
        {
            var res = await _repository.Find(query.Id); 
            if (res == null)
            {
                throw new UserFriendlyException("اطلاعات یافت نشد");
            }
            return res.GetRequestOrgDto();
        }
    }
}
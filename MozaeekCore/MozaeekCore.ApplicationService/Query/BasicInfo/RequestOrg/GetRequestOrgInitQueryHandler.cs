using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;
using MozaeekCore.Mapper;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetRequestOrgInitQueryHandler : IBaseAsyncQueryHandler<Nothing, InitRequestOrgDto>
    {
        private readonly IGenericRepository<RequestOrg> _repository;

        public GetRequestOrgInitQueryHandler(IGenericRepository<RequestOrg> repository)
        {
            _repository = repository;
        }
        public async Task<InitRequestOrgDto> HandleAsync(Nothing query)
        {
            var res = (await _repository.GetAll().Select(m => BasicInfoProfile.GetRequestOrgDtoNoParent(m)).ToListAsync());
            return new InitRequestOrgDto()
            {
                RequestOrgs = res
            };
        }
    }
}
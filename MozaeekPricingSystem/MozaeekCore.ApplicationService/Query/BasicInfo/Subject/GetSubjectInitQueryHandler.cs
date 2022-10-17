using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;
using MozaeekCore.Mapper;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetSubjectInitQueryHandler : IBaseAsyncQueryHandler<Nothing, InitSubjectDto>
    {
        private readonly IGenericRepository<Subject> _repository;

        public GetSubjectInitQueryHandler(IGenericRepository<Subject> repository)
        {
            _repository = repository;
        }
        public async Task<InitSubjectDto> HandleAsync(Nothing query)
        {
            var res = (await _repository.GetAll().Select(m => BasicInfoProfile.GetSubjectDtoNoParent(m)).ToListAsync());
            return new InitSubjectDto()
            {
                Subjects = res
            };
        }
    }
}
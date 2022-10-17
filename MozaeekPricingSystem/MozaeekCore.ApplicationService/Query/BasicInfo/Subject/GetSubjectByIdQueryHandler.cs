using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;
using MozaeekCore.Exceptions;
using MozaeekCore.Mapper;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetSubjectByIdQueryHandler : IBaseAsyncQueryHandler<FindByKey, SubjectDto>
    {
        private readonly IGenericRepository<Subject> _repository;

        public GetSubjectByIdQueryHandler(IGenericRepository<Subject> repository)
        {
            _repository = repository;
        }
        public async Task<SubjectDto> HandleAsync(FindByKey query)
        {
            var res = await _repository.Find(query.Id);
            if (res == null)
            {
                throw new UserFriendlyException("اطلاعات یافت نشد");
            }
            return res.GetSubjectDto();
        }
    }
}
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetTechnicianSubjectDtoQueryHandler : IBaseAsyncQueryHandler<FindByKey, TechnicianSubjectDto>
    {
        private readonly ITechnicianRepository repository;

        public GetTechnicianSubjectDtoQueryHandler(ITechnicianRepository repository)
        {
            this.repository = repository;
        }
        public async Task<TechnicianSubjectDto> HandleAsync(FindByKey query)
        {
            var res = await repository.FindWithSubject(query.Id);
            var info = new TechnicianSubjectDto()
            {
                TechnicianId = res.Id
            };

            if (res.TechnicianSubjects != null)
            {
                info.Subjects = res.TechnicianSubjects.Select(m => m.SubjectId).ToList();
            }

            return info;
        }
    }
}
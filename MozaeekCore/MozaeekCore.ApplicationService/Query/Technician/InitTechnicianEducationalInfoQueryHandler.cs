using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;
using MozaeekCore.Mapper;

namespace MozaeekCore.ApplicationService.Query
{
    public class InitTechnicianEducationalInfoQueryHandler : IBaseAsyncQueryHandler<Nothing, InitTechnicianEducationalInfo>
    {
        private readonly IGenericRepository<EducationField> _educationfieldRepository;
        private readonly IGenericRepository<EducationGrade> _educationgradeRepository;

        public InitTechnicianEducationalInfoQueryHandler(IGenericRepository<EducationField> educationfieldRepository, IGenericRepository<EducationGrade> educationgradeRepository)
        {
            _educationfieldRepository = educationfieldRepository;
            _educationgradeRepository = educationgradeRepository;
        }

        public async Task<InitTechnicianEducationalInfo> HandleAsync(Nothing query)
        {
            var educationField = await _educationfieldRepository.GetAll().Select(m => TechnicianProfile.GetEducationFieldDto(m)).ToListAsync();
            var educationGrade = await _educationgradeRepository.GetAll().Select(m => m.GetEducationGradeDto()).ToListAsync();
            return new InitTechnicianEducationalInfo()
            {
                EducationFields = educationField,
                EducationGrades = educationGrade
            };
        }
    }
}
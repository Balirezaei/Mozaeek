using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Domain;

namespace MozaeekCore.Mapper
{
    public static class TechnicianProfile
    {
        public static EducationFieldDto GetEducationFieldDto(this EducationField domain)
        {
            return new EducationFieldDto()
            {
                Id = domain.Id,
                Title = domain.Title
            };
        }

        public static EducationGradeDto GetEducationGradeDto(this EducationGrade domain)
        {
            return new EducationGradeDto()
            {
                Id = domain.Id,
                Title = domain.Title
            };
        }
    }
}
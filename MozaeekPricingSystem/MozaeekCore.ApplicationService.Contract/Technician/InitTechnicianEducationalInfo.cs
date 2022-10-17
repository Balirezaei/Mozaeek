using System.Collections.Generic;

namespace MozaeekCore.ApplicationService.Contract
{
    public class InitTechnicianEducationalInfo
    {
        public List<EducationGradeDto> EducationGrades { get; set; }
        public List<EducationFieldDto> EducationFields { get; set; }
    }
}
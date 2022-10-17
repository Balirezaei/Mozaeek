using MozaeekCore.Core.Base;
using MozaeekCore.Domain;

namespace MozaeekCore.ApplicationService.Contract
{
    public class AddingToTechnicianEducationalInfoCommand : Command
    {
        public long TechnicianId { get; set; }
        public long EducationGradeId { get; set; }
        public long EducationFieldId { get; set; }
    }
}
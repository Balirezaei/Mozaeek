using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class AddingToTechnicianSubjectCommand : Command
    { public long TechnicianId { get; set; }
        public long[] SubjectId { get; set; }
    }
}
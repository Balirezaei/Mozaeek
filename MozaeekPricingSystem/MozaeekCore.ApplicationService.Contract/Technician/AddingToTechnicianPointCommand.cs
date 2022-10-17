using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class AddingToTechnicianPointCommand : Command
    {
        public long TechnicianId { get; set; }
        public long[] PointId { get; set; }
    }
}
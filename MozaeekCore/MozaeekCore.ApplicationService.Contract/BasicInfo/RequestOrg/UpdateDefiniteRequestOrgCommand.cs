using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class UpdateDefiniteRequestOrgCommand : Command
    {
        public long Id { get; set; }
        public long PointId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
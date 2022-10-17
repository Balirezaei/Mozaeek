using MozaeekCore.QueryModel;

namespace MozaeekCore.ApplicationService.Contract
{
    public class TechnicianDashboardContract : PagingContract
    {
        public long TechnicianId { get; set; }
    }
}
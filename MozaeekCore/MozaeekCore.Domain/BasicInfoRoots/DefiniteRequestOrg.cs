using MozaeekCore.Common.ExtensionMethod;
using System.Collections.Generic;

namespace MozaeekCore.Domain
{
    public class DefiniteRequestOrg : BasicInfo
    {
        public long RequestOrgId { get; private set; }
        public virtual RequestOrg RequestOrg { get; private set; }

        public long PointId { get; private set; }
        public virtual Point Point { get; private set; }
        public string Address { get; private set; }
        public string PhoneNumber { get; private set; }
        public virtual ICollection<TechnicianDefiniteRequestOrg> TechnicianDefiniteRequestOrgs { get; set; }
        public virtual ICollection<RequestDefiniteRequestOrg> RequestDefiniteRequestOrgs { get; set; }
        protected DefiniteRequestOrg()
        {

        }

        public DefiniteRequestOrg(long id, long requestOrgId, long pointId, string address, string phoneNumber)
        {
            Id = id;
            RequestOrgId = requestOrgId;
            PointId = pointId;
            Address = address.Recheck();
            PhoneNumber = phoneNumber.Recheck();
        }

        public void Update(string address, string phoneNumber, long pointId)
        {
            PointId = pointId;
            Address = address.Recheck();
            PhoneNumber = phoneNumber.Recheck();
        }
    }
}
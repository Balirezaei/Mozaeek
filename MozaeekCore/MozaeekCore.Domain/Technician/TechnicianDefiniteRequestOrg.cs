using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MozaeekCore.Domain
{
    public class TechnicianDefiniteRequestOrg
    {
        public long Id { get; set; }
        public virtual DefiniteRequestOrg DefiniteRequestOrg{ get; set; }
        public long DefiniteRequestOrgId { get; set; }
        public virtual Technician Technician { get; set; }
        public long TechnicianId { get; set; }
    }
}

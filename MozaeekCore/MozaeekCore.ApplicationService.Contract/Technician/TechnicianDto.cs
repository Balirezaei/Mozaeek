using System;
using System.Collections.Generic;
using System.Text;
using Mozaeek.CR.PublicDto;
using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class TechnicianDto
    {
        public long Id { get;  set; }
        public string PhoneNumber { get;  set; }
        public string FirstName { get;  set; }
        public string LastName { get;  set; }
        public string Email { get;  set; }
        public string Address { get;  set; }
        public string NationalId { get; set; }
        public string PostalCode { get; set; }
        public long? PointId { get; set; }
        public TechnicianType TechnicianType { get; set; }
        public List<long> OfflineRequestTargetIds { get; set; }
        public List<long> DefiniteRequestOrgIds { get; set; }
        public List<long> SubjectIds { get; set; }
        public List<TechnicianAttachmentDto> Attachments { get; set; }
        public bool FirstVerification { get; set; }
        public bool SecondVerification { get; set; }
        public DateTime CreateDateTime { get; set; }
    }

}

using System;
using System.Collections.Generic;

namespace MozaeekCore.ApplicationService.Contract
{
    public class RequestDto
    {
        public long Id { get; set; }
        public long RequestTargetId { get; set; }
        public long RequestActId { get; set; }
        public List<RequestActionDto> RequestActions { get; set; }
        public List<RequestNecessityDto> RequestNecessities { get; set; }
        public List<RequestQualificationDto> RequestQualifications { get; set; }
        public List<RequestTargetConnectionDto> ConnectionDtos { get; set; }
        public List<long> Points { get; set; }
        public List<long> RequestOrgs { get; set; }
        public List<long> DefiniteRequestOrgs { get; set; }
        public DateTime? RequestExpiredDate { get; set; }
        public DateTime? RequestStartDate { get; set; }
        public bool FullOnline { get; set; }
        public string Summary { get; set; }
        public string Regulation { get; set; }
        
    }
}
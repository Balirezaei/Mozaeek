using System;
using System.Collections.Generic;
using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class UpdateRequestCommand : Command
    {
        public UpdateRequestCommand()
        {
            this.RequestNecessities = new List<RequestNecessityDto>();
            this.RequestQualifications = new List<RequestQualificationDto>();
            this.RequestActions = new List<RequestActionDto>();
            this.ConnectionDtos = new List<RequestTargetConnectionDto>();
            Points = new List<PointDto>();
            RequestOrgs = new List<RequestOrgDto>();
            DefiniteRequestOrgDtos = new List<DefiniteRequestOrgDto>();
        }

        public long Id { get; set; }
        public long RequestTargetId { get; set; }
        public long RequestActId { get; set; }
        public List<RequestActionDto> RequestActions { get; set; }
        public List<RequestNecessityDto> RequestNecessities { get; set; }
        public List<RequestOrgDto> RequestOrgs { get; set; }
        public List<DefiniteRequestOrgDto> DefiniteRequestOrgDtos { get; set; }
        public List<RequestQualificationDto> RequestQualifications { get; set; }
        public List<RequestTargetConnectionDto> ConnectionDtos { get; set; }
        public List<PointDto> Points { get; set; }

        public DateTime? RequestExpiredDate { get; set; }

        public DateTime? RequestStartDate { get; set; }
        public bool FullOnline { get; set; }
        public string Summary { get; set; }
        public string Regulation { get; set; }
    }
}
using System;
using System.Collections.Generic;
using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class UpdateRequestCommand : Command
    {
        public UpdateRequestCommand()
        {
            this.RequestNessesities = new List<RequestNessesityDto>();
            this.RequestQualifications = new List<RequestQualificationDto>();
            this.RequestActions = new List<RequestActionDto>();
            this.RequestDocuments = new List<RequestDocumentDto>();
        }

        public long Id { get; set; }
        public long RequestTargetId { get; set; }
        public long RequestActId { get; set; }
        public List<RequestActionDto> RequestActions { get; set; }
        public List<RequestNessesityDto> RequestNessesities { get; set; }
        public List<RequestDocumentDto> RequestDocuments { get; set; }
        public List<RequestQualificationDto> RequestQualifications { get; set; }
        public List<PointDto> Points { get; set; }

        public DateTime? RequestExpiredDate { get; set; }

        public DateTime? RequestStartDate { get; set; }
    }
}
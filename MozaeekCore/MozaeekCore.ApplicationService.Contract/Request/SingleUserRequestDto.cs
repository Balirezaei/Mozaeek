using System.Collections.Generic;
using MozaeekCore.Domain.Contract.Request.Events;

namespace MozaeekCore.ApplicationService.Contract
{
    public class SingleUserRequestDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long RequestActId { get; set; }
        public long RequestPointId { get; set; }

        public int PriceAmount { get; set; }
        public int TechnicianCount { get; set; }
        public List<RequestActDto> RequestActs { get; set; }

        /// <summary>
        /// اقدامات یا مراحل
        /// </summary>
        public List<RequestActionDto> RequestActions { get; set; }

        /// <summary>
        /// بایسته ها
        /// </summary>
        public List<RequestNecessityDto> RequestNecessities { get; set; }


        /// <summary>
        /// شرایط
        /// </summary>
        public List<RequestQualificationDto> RequestQualifications { get; set; }

        public List<PointDto> Points { get; set; }
    }
}
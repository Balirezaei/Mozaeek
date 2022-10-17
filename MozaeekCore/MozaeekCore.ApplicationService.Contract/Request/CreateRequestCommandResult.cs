using System.Collections.Generic;

namespace MozaeekCore.ApplicationService.Contract
{
    public class CreateRequestCommandResult
    {
        public long Id { get; set; }
    }

    public class RequestGrid
    {
        public long Id { get; set; }
        public string Title { get; set; }

    }

    public class RequestListGroupByRequestTarget
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long RequestActId { get; set; }

        public List<RequestActDto> RequestActs { get; set; }
        public List<PointDto> Points { get; set; }
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

    }
}
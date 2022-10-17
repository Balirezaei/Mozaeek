using System.Collections.Generic;

namespace MozaeekTechnicianProfile.ApplicationService.Contract.SearchDto
{
    public class GetProperTechnicianDto
    {
        public List<long> Subjects { get; set; }
        public List<long> OnlineRequesId { get; set; }
        public List<long> OflineRequestId { get; set; }
    }

    public class ProperTechnicianResult
    {
        public long Id { get; set; }
        public string FullName { get; set; }
    }
}
using System.Collections.Generic;

namespace MozaeekCore.ApplicationService.Contract
{
    public class RequestTargetGrid
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public List<string> Labels { get; set; }
        public List<string> RequestOrgs { get; set; }
        public List<string> Subjects { get; set; }
    }
}
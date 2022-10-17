using System.Collections.Generic;

namespace MozaeekCore.ApplicationService.Contract
{
    public class RequestTargetDto
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public long Id { get; set; }
        public List<long> Labels { get; set; }
        //public List<long> RequestOrgs { get; set; }
        public List<long> Subjects { get; set; }
        public bool IsDocument { get; set; }

    }
}
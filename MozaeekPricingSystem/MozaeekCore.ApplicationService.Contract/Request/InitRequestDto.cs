using System.Collections.Generic;

namespace MozaeekCore.ApplicationService.Contract
{
    public class InitRequestDto
    {
        public List<SmallRequestTargetDto> RequestTargets { get; set; }
        public List<PointDto> Points { get; set; }
    }

    public class SmallRequestTargetDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
    }
}
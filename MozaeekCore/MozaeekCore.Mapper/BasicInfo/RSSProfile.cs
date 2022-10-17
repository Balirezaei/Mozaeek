using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Domain;

namespace MozaeekCore.Mapper
{
    public static class RSSProfile
    {
        public static RSSDto GetRSSDto(this RSS domain)
        {
            return new RSSDto()
            {
                Source = domain.Source,
                Url = domain.Url,
                IsActive = domain.IsActive,
                Id = domain.Id,
                IntervalDataReceiveHours = domain.IntervalDataReceiveHours
            };
        }
    }
}
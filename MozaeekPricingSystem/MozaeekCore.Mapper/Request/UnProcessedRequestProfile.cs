using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Domain;

namespace MozaeekCore.Mapper
{
    public static class UnProcessedRequestMapping
    {
        public static UnProcessedRequestDto GetUnProcessedRequestDto(this UnProcessedRequest domain)
        {
            return new UnProcessedRequestDto
            {
                Id = domain.Id,
                IsProcessed = domain.IsProcessed,
                Summery = domain.Summery,
                Title = domain.Title,
            }; 
        }
    }
}
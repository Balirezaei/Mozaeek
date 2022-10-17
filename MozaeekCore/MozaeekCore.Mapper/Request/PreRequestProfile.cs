using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Domain;

namespace MozaeekCore.Mapper
{
    public static class PreRequestMapping
    {
        public static PreRequestDto GetPreRequestDto(this PreRequest domain)
        {
            return new PreRequestDto
            {
                Id = domain.Id,
                IsProcessed = domain.IsProcessed,
                Summary = domain.Summary,
                Title = domain.Title,
            }; 
        }
    }
}
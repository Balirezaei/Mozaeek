using MozaeekUserProfile.ApplicationService.Contract;
using MozaeekUserProfile.ApplicationService.Contract.Dtos;
using MozaeekUserProfile.Domain;

namespace MozaeekUserProfile.Mapper
{
    public static class UserProfileMapping
    {
       

        public static UserProfileOutputDto GetUserInput(this User domain)
        {
            return new UserProfileOutputDto()
            {
                PhoneNumber = domain.PhoneNumber,
                FirstName = domain.FirstName,
                LastName = domain.LastName,
                Address = domain.Address,
                Email = domain.Email,
                Id = domain.Id
            };
        }

    }
}
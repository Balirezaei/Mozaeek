using System.ComponentModel.DataAnnotations;

namespace MozaeekUserProfile.ApplicationService.Contract.Dtos
{
    public class UserProfileCompleteInputeDto
    {
        [MinLength(2)]
        [Required]
        public string FirstName { get; set; }
        [MinLength(2)]
        [Required]
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage = "Enter Valid Email")]
        public string Email { get; set; }

        public string Address { get; set; }
        public long UserId { get; set; }
    }
}

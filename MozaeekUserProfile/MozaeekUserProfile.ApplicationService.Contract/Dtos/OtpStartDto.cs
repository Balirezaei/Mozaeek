using System.ComponentModel.DataAnnotations;
using MozaeekUserProfile.ApplicationService.Contract.Validators;

namespace MozaeekUserProfile.ApplicationService.Contract.Dtos
{
    public class OtpStartDto
    {
        [Required]
        [ValidateMobileNo]
        public string MobileNo { get; set; }
    }
}

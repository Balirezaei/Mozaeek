using System.ComponentModel.DataAnnotations;
using MozaeekTechnicianProfile.ApplicationService.Contract.Validators;

namespace MozaeekTechnicianProfile.ApplicationService.Contract.Dtos
{
    public class OtpStartDto
    {
        [Required]
        [ValidateMobileNo]
        public string MobileNo { get; set; }
    }
}

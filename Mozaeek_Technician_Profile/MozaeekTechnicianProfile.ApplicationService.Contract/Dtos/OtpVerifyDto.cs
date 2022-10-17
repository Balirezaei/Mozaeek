using System.ComponentModel.DataAnnotations;
using MozaeekTechnicianProfile.ApplicationService.Contract.Validators;
using MozaeekTechnicianProfile.Common.Constants;

namespace MozaeekTechnicianProfile.ApplicationService.Contract.Dtos
{
    public class OtpVerifyDto
    {
        [MinLength(SettingConstants.OtpCodeLength)]
        [Required]
        public string Code { get; set; }
        [Required]
        [ValidateMobileNo]
        public string MobileNo { get; set; }
    }
}

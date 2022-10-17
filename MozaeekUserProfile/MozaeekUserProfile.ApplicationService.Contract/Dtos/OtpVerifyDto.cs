using System.ComponentModel.DataAnnotations;
using MozaeekUserProfile.ApplicationService.Contract.Validators;
using MozaeekUserProfile.Common.Constants;

namespace MozaeekUserProfile.ApplicationService.Contract.Dtos
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

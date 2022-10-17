using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MozaeekUserProfile.ApplicationService.Contract.Dtos
{
    public class UserProfileUpdateDeviceIdDto
    {
        [Required]
        public string DeviceId { get; set; }
        [Required]
        public long UserId { get; set; }
    }
}

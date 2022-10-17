using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MozaeekTechnicianProfile.ApplicationService.Contract.Dtos;
using MozaeekTechnicianProfile.ApplicationService.Services.OtpServices;
using MozaeekTechnicianProfile.RestAPI.Services;
using Newtonsoft.Json;

namespace MozaeekTechnicianProfile.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OTPController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IOtpStarterService _otpStarterService; 
        private readonly IOtpVerifierService _otpVerifierService;

        public OTPController(ITokenService tokenService, IOtpStarterService otpStarterService, IOtpVerifierService otpVerifierService)
        {
            _tokenService = tokenService;
            _otpStarterService = otpStarterService;
            _otpVerifierService = otpVerifierService;
        }

        [HttpPost("start")]
        public Task StartOtp( OtpStartDto otpStartDto)
        {
            return _otpStarterService.StartSession(otpStartDto.MobileNo);
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyOtp( OtpVerifyDto otpVerifyDto)
        {
            var refreshToken = _tokenService.GenerateRefreshToken();

            var user = await _otpVerifierService.Verify(otpVerifyDto.Code, otpVerifyDto.MobileNo, refreshToken);
       
            var userDate =
                JsonConvert.SerializeObject(user);
            var usersClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.UserData,userDate),
                new Claim(ClaimTypes.NameIdentifier,user.PhoneNumber),
            };
            var jwtToken = _tokenService.GenerateAccessToken(usersClaims);
          
            return new ObjectResult(new
            {
                token = jwtToken,
                refreshToken = refreshToken
            });
        }
    }
}

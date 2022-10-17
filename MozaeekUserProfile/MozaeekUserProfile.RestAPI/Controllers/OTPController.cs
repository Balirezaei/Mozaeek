using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MozaeekUserProfile.ApplicationService.Services.OtpServices;
using System.Threading.Tasks;
using MozaeekUserProfile.ApplicationService.Contract.Dtos;
using MozaeekUserProfile.Domain.Contracts;
using MozaeekUserProfile.RestAPI.Extensions;
using Newtonsoft.Json;

namespace MozaeekUserProfile.RestAPI.Controllers
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

using Microsoft.AspNetCore.Mvc;
using MozaeekUserProfile.Domain.Contracts;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MozaeekUserProfile.ApplicationService.Contract.Dtos;
using MozaeekUserProfile.ApplicationService.Services.OtpServices;
using MozaeekUserProfile.RestAPI.Extensions;
using Newtonsoft.Json;

namespace MozaeekUserProfile.RestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IOtpVerifierService _otpVerifierService;

        public TokenController(ITokenService tokenService, IOtpVerifierService otpVerifierService)
        {
            _tokenService = tokenService;
            _otpVerifierService = otpVerifierService;
        }
        [HttpPost]
        public IActionResult Validate([FromBody] TokenInput token)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(token.Token);
            if (principal == null)
            {
                throw new System.Exception("Invalid Token");
            }
            var mobileNoClaim = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (mobileNoClaim == null)
            {
                throw new System.Exception("Invalid Token");
            }

            return new ObjectResult(new { Message = mobileNoClaim.Value });
        }


        [HttpPost]
        // [Authorize]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenInput input)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(input.Token);

            var jsonUserData = principal.Claims.FirstOrDefault(m => m.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata").Value;

            var userDate = JsonConvert.DeserializeObject<UserLoginDto>(jsonUserData);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            await _otpVerifierService.CheckRefreshTokenAndReplaceNew(userDate.UserId, input.RefreshToken, input.RefreshToken);
            
            var claims = principal.Claims.Where(m => m.Type != "aud");

            var newJwtToken = _tokenService.GenerateAccessToken(claims);

            return new ObjectResult(new
            {
                token = newJwtToken,
                refreshToken = input.RefreshToken
            });
        }
    }
    public class TokenInput
    {
        public string Token { get; set; }
    }

    public class RefreshTokenInput
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}

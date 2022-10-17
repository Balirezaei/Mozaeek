using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MozaeekCore.Core.CommandBus;
using MozaeekCore.RestAPI.Utility;
using Newtonsoft.Json;
using MozaeekCore.Domain.Identity;
using MozaeekCore.Exceptions;
using MozaeekCore.Facade.Contract;

namespace MozaeekCore.RestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly ICommandBus _commandBus;
        private readonly IUserRepository userRepository;

        public AccountController(ITokenService tokenService,
                                 ICommandBus commandBus,
                                 IUserRepository userRepository)
        {
            _tokenService = tokenService;
            _commandBus = commandBus;
            this.userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await userRepository.CheckAndGet(loginDto.UserName, loginDto.Password);
            var userDate =
                JsonConvert.SerializeObject(new
                {
                    UserId = user.Id,
                    userName = user.UserName
                });

            var usersClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginDto.UserName),
                new Claim(ClaimTypes.UserData,userDate) ,
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            if (user.UserRoles.Any())
            {
                foreach (var role in user.UserRoles)
                {
                    usersClaims.Add(new Claim(ClaimTypes.Role, role.Role.ToString()));
                }
            }
            var jwtToken = _tokenService.GenerateAccessToken(usersClaims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            await userRepository.UpdateUserTokenExpired(user.Id, refreshToken);

            return new ObjectResult(new
            {
                token = jwtToken,
                refreshToken = refreshToken
            });

        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken(RefreshTokenDto refreshToken)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(refreshToken.Token);
            var username = principal.Identity.Name; //this is mapped to the Name claim by default

            var user = await userRepository.GetUserByUserName(username);
            if (user.LastExpiredToken != refreshToken.RefreshToken)
            {
                throw new UserFriendlyException("رفرش توکن نادرست است.");
            }
            var claims = principal.Claims.Where(m => m.Type != "aud");
            var newJwtToken = _tokenService.GenerateAccessToken(claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            await userRepository.UpdateUserTokenExpired(user.Id, newRefreshToken);

            return new ObjectResult(new
            {
                token = newJwtToken,
                refreshToken = newRefreshToken
            });
        }

        [Authorize]
        [HttpGet]
        public IActionResult AuthorizedRequest()
        {
            return Content("You Are Authorized" +
                           HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name));
        }
    }
}

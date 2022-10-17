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

namespace MozaeekCore.RestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly ICommandBus _commandBus;
        // private readonly IUserQueryFacade _userQueryFacade;


        public AccountController(ITokenService tokenService, ICommandBus commandBus)
        {
            _tokenService = tokenService;
            // _userQueryFacade = userQueryFacade;
            _commandBus = commandBus;
        }

        [HttpGet]
        public IActionResult Login(string username, string password)
        {
            //To Do : handle this query with queryHandler

            // var loginResult = _userQueryFacade.LoginUser(new ApplicationService.Contract.LoginUserQuery(username, password));
            //
            // if (loginResult == null)
            // {
            //     return new ObjectResult(new
            //     {
            //         error = "نام کاربری و کلمه ی عبور نادرست است."
            //     });
            // }

            var userDate =
                JsonConvert.SerializeObject(new
                {
                    UserId = new Random().Next(1,1000),
                    userName = "User"
                });

            var usersClaims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.UserData,userDate) ,
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role,"CanDeactiveUser"),
                new Claim(ClaimTypes.Role,"CanCreateUser"),
                new Claim(ClaimTypes.Role,"CanEditUser"),
                new Claim(ClaimTypes.Role,"CanViewUser"),
            };

            var jwtToken = _tokenService.GenerateAccessToken(usersClaims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            //            return new Json(new {Token= jwtToken});

            return new ObjectResult(new
            {
                token = jwtToken,
                refreshToken = refreshToken,
                // menu = loginResult.Menu
            });
        }
        
        [HttpGet]
        public IActionResult RefreshToken(string token, string refreshToken)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(token);
            var username = principal.Identity.Name; //this is mapped to the Name claim by default

            //            var user = _usersDb.Users.SingleOrDefault(u => u.Username == username);
            //            if (user == null || user.RefreshToken != refreshToken)

            var newJwtToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            //            user.RefreshToken = newRefreshToken;
            //            await _usersDb.SaveChangesAsync();

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

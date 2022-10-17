using Microsoft.AspNetCore.Mvc;
using MozaeekUserProfile.Domain.Contracts;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MozaeekUserProfile.RestAPI.Extensions;
using Newtonsoft.Json;

namespace MozaeekUserProfile.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public TestController(IUserRepository userRepository, ITokenService tokenService)
        {
            this._userRepository = userRepository;
            _tokenService = tokenService;
        }
        [HttpGet("{id}")]
        public async Task<string> GetToken(int id)
        {
            var user = await _userRepository.GetUserById(id);
            var userDate =
                JsonConvert.SerializeObject(user);
            var usersClaims = new List<Claim>
           {
               new Claim(ClaimTypes.Name, user.PhoneNumber),
               new Claim(ClaimTypes.UserData,userDate)  ,
               new Claim(ClaimTypes.NameIdentifier,user.PhoneNumber),
           };
            return _tokenService.GenerateAccessToken(usersClaims);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MozaeekTechnicianProfile.Domain;
using MozaeekTechnicianProfile.RestAPI.Extensions;
using MozaeekTechnicianProfile.RestAPI.Services;
using Newtonsoft.Json;

namespace MozaeekTechnicianProfile.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITechnicianRepository _repository;
        private readonly ITokenService _tokenService;

        public TestController(ITechnicianRepository repository, ITokenService tokenService)
        {
            this._repository = repository;
            _tokenService = tokenService;
        }
        // [HttpGet("{id}")]
        // public async Task<string> GetToken(int id)
        // {
        //     var user = await _repository.GetUserById(id);
        //     var userDate =
        //         JsonConvert.SerializeObject(user);
        //     var usersClaims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, user.PhoneNumber),
        //        new Claim(ClaimTypes.UserData,userDate)  ,
        //        new Claim(ClaimTypes.NameIdentifier,user.PhoneNumber),
        //    };
        //     return _tokenService.GenerateAccessToken(usersClaims);
        // }
    }
}

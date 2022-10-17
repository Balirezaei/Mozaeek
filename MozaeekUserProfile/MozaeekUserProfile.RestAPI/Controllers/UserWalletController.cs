using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MozaeekUserProfile.ApplicationService.Contract;
using MozaeekUserProfile.ApplicationService.Services;


namespace MozaeekUserProfile.RestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserWalletController : ControllerBase
    {
        private readonly IUserWalletService _userWalletService;

        public UserWalletController(IUserWalletService userWalletService)
        {
            _userWalletService = userWalletService;
        }
        
        [HttpGet]
        public Task<CurrentBalance> GetCurrentBalance(long userId)
        {
            return _userWalletService.GetCurrentBalance(userId);
        }
    }
}

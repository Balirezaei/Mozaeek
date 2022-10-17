using System.Threading.Tasks;
using Mozaeek.CR.PublicDto.Enum;
using MozaeekUserProfile.ApplicationService.Contract;
using MozaeekUserProfile.Domain;

namespace MozaeekUserProfile.ApplicationService.Services
{
    public interface IUserWalletService
    {
        Task<CurrentBalance> GetCurrentBalance(long userId);
    }
    public class UserWalletService : IUserWalletService
    {
        private readonly IUserWalletRepository _userWalletRepository;

        public UserWalletService(IUserWalletRepository userWalletRepository)
        {
            _userWalletRepository = userWalletRepository;
        }

        public async Task<CurrentBalance> GetCurrentBalance(long userId)
        {
            var amount = await _userWalletRepository.GetByCurrency(userId, (long)PriceCurrencyType.Rial);
            return new CurrentBalance()
            {
                AvailableAmount = amount != null ? amount.AvailableAmount : 0
            };
        }
    }
}
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekUserProfile.Persistense.EF;

namespace MozaeekUserProfile.Domain.Service
{
    public interface IUserWalletDomainService
    {
        Task<bool> UserHascredit(long userId, long price, int currencyType);
    }

    //public class UserWalletDomainService : IUserWalletDomainService
    //{
    //    private readonly MozaeekUserProfileContext _context;
    //    public UserWalletDomainService(MozaeekUserProfileContext context)
    //    {
    //        _context = context;
    //    }

    //    public Task<bool> UserHascredit(long userId, long price, int currencyType)
    //    {
    //        _context.UserWallets.fi()
    //    }
    //}
}
using System;
using Microsoft.EntityFrameworkCore;
using MozaeekUserProfile.Domain;
using MozaeekUserProfile.Domain.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace MozaeekUserProfile.Persistense.EF.Repository
{
    public class OtpCodeSqlRepository : IOtpCodeRepository
    {
        private readonly MozaeekUserProfileContext dbContext;

        public OtpCodeSqlRepository(MozaeekUserProfileContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<bool> CheckAndDelete(string mobileNo, string code)
        {
            var otpCodes = await dbContext.OtpCodes.Where(x => x.MobileNo == mobileNo && x.ExpiredDate >= DateTime.Now).ToListAsync();
            if (otpCodes == null || otpCodes.Count == 0)
            {
                return false;
            }

            var otpCode = otpCodes.FirstOrDefault(x => x.Code == code);
            if (otpCode == null)
            {
                return false;
            }

            // dbContext.OtpCodes.RemoveRange(otpCodes);

            return true;
        }

        public Task Store(string otpCode, string mobileNo)
        {
            dbContext.OtpCodes.Add(new OtpCode(mobileNo, otpCode));
            return dbContext.SaveChangesAsync();
        }
    }
}

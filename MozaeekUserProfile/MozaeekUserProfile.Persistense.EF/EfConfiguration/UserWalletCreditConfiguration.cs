using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekUserProfile.Domain;

namespace MozaeekUserProfile.Persistense.EF.EfConfiguration
{
    public class UserWalletCreditConfiguration : IEntityTypeConfiguration<UserWalletCredit>
    {
        public void Configure(EntityTypeBuilder<UserWalletCredit> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Description).HasMaxLength(250);

            builder.HasOne(m => m.UserWallet)
                .WithMany(m => m.UserWalletCredits)
                .HasForeignKey(m => m.UserWalletId);
            
        }
    }
}
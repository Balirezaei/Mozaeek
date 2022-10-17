using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekUserProfile.Domain;

namespace MozaeekUserProfile.Persistense.EF.EfConfiguration
{
    public class UserWalletConfiguration : IEntityTypeConfiguration<UserWallet>
    {
        public void Configure(EntityTypeBuilder<UserWallet> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.PriceCurrencyTitle).HasMaxLength(50);
            builder.HasOne(m => m.User)
                .WithMany(m => m.UserWallets)
                .HasForeignKey(m => m.UserId);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekUserProfile.Domain;

namespace MozaeekUserProfile.Persistense.EF.EfConfiguration
{
    public class UserWalletDebitConfiguration : IEntityTypeConfiguration<UserWalletDebit>
    {
        public void Configure(EntityTypeBuilder<UserWalletDebit> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Description).HasMaxLength(250);

            builder.HasOne(m => m.UserWallet)
                .WithMany(m => m.UserWalletDebits)
                .HasForeignKey(m => m.UserWalletId);

            builder.HasOne(m => m.UserQuestions)
                .WithOne(m => m.UserWalletDebit)
                .HasForeignKey<UserWalletDebit>(m => m.UserQuestionId);
        }
    }
}
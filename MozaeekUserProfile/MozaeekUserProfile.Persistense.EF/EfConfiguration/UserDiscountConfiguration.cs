using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekUserProfile.Domain;

namespace MozaeekUserProfile.Persistense.EF.EfConfiguration
{
    public class UserDiscountConfiguration : IEntityTypeConfiguration<UserDiscount>
    {
        public void Configure(EntityTypeBuilder<UserDiscount> builder)
        {
            builder.ToTable("UserDiscount");
            
            builder.HasOne(m => m.User)
                .WithMany(m => m.UserDiscounts)
                .HasForeignKey(m => m.UserId);

        }
    }
}
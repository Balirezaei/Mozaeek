using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekUserProfile.Domain;

namespace MozaeekUserProfile.Persistense.EF.EfConfiguration
{
    public class UserProfileCharacteristicOwnerConfiguration:IEntityTypeConfiguration<UserProfileCharacteristicOwner>
    {
        public void Configure(EntityTypeBuilder<UserProfileCharacteristicOwner> builder)
        {
            builder.ToTable("UserProfileCharacteristicOwner");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Name)
                .HasMaxLength(30);


            builder.HasOne(m => m.User)
                .WithMany(m => m.UserProfileCharacteristicOwners)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
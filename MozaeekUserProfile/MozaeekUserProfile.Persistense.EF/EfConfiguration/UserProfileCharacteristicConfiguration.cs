using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekUserProfile.Domain;

namespace MozaeekUserProfile.Persistense.EF.EfConfiguration
{
    public class UserProfileCharacteristicConfiguration : IEntityTypeConfiguration<UserProfileCharacteristic>
    {
        public void Configure(EntityTypeBuilder<UserProfileCharacteristic> builder)
        {
            builder.ToTable("UserProfileCharacteristic");
            builder.HasKey(m => m.Id);

            builder.Property(m => m.LabelTitle)
                .HasMaxLength(40);
            builder.Property(m => m.FirstLabelParentTitle)
                .HasMaxLength(40);
            

            builder.HasOne(m => m.UserProfileCharacteristicOwner)
                .WithMany(m => m.UserProfileCharacteristics)
                .HasForeignKey(m => m.UserProfileCharacteristicOwnerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
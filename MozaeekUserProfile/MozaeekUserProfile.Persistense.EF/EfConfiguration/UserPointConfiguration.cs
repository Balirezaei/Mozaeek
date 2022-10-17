using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekUserProfile.Domain;

namespace MozaeekUserProfile.Persistense.EF.EfConfiguration
{
    public class UserPointConfiguration:IEntityTypeConfiguration<UserPoint>
    {
        public void Configure(EntityTypeBuilder<UserPoint> builder)
        {
            builder.ToTable("UserPoint");
            builder.HasKey(m => m.Id);

            builder
                .HasOne(m => m.User)
                .WithMany(m => m.UserPoints)
                .HasForeignKey(m => m.UserId);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekUserProfile.Domain;

namespace MozaeekUserProfile.Persistense.EF.EfConfiguration
{
    public class OtpCodeConfiguration : IEntityTypeConfiguration<OtpCode>
    {
        public void Configure(EntityTypeBuilder<OtpCode> builder)
        {
            builder.ToTable("OtpCode");
            builder.HasKey(m => m.Id);
            builder.Property(s => s.Code)
                .IsRequired(true).HasMaxLength(8);
            builder.Property(s => s.MobileNo)
                .IsRequired(true).HasMaxLength(12);

        }
    }
}

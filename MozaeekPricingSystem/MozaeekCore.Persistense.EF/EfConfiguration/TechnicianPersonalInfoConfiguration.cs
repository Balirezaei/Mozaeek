using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekCore.Domain;

namespace MozaeekCore.Persistense.EF.EfConfiguration
{
    public class TechnicianPersonalInfoConfiguration : IEntityTypeConfiguration<TechnicianPersonalInfo>
    {
        public void Configure(EntityTypeBuilder<TechnicianPersonalInfo> builder)
        {
            builder.ToTable("TechnicianPersonalInfo");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.FirstName).HasMaxLength(100);
            builder.Property(m => m.LastName).HasMaxLength(100);
            builder.Property(m => m.IdentityNumber).HasMaxLength(20);
            builder.Property(m => m.NationalCode).HasMaxLength(20);
        }
    }
}
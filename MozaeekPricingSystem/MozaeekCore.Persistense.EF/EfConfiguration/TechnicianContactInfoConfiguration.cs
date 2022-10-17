using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekCore.Domain;
namespace MozaeekCore.Persistense.EF.EfConfiguration
{
    public class TechnicianContactInfoConfiguration : IEntityTypeConfiguration<TechnicianContactInfo>
    {
        public void Configure(EntityTypeBuilder<TechnicianContactInfo> builder)
        {
            builder.ToTable("TechnicianContactInfo");
            
            builder.HasKey(m => m.Id);
            
            builder.Property(m => m.Address)
                .HasMaxLength(250);

            builder
                .Property(m => m.PhoneNumber)
                .HasMaxLength(20);

            builder
                .Property(m => m.OfficeNumber)
                .HasMaxLength(20);
            builder
                .Property(m => m.MobileNumber)
                .HasMaxLength(20);
        }
    }
}
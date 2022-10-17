using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekCore.Domain;

namespace MozaeekCore.Persistense.EF.EfConfiguration
{
    public class TechnicianAttachmentConfiguration : IEntityTypeConfiguration<TechnicianAttachment>
    {
        public void Configure(EntityTypeBuilder<TechnicianAttachment> builder)
        {
            builder.ToTable("TechnicianAttachment");
            builder.HasKey(m => m.Id);

            builder.Property(m => m.FileName).HasMaxLength(100);
            builder.Property(m => m.FileExtention).HasMaxLength(50);

        }
    }
}
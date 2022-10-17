using Karmizban.Support.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karmizban.Support.EF.EfConfiguration
{
    public class TechnicianRequestSupportConfiguration : IEntityTypeConfiguration<TechnicianRequestSupport>
    {
        public void Configure(EntityTypeBuilder<TechnicianRequestSupport> builder)
        {
            builder.ToTable("TechnicianRequestSupport");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Description)
                .HasMaxLength(2500);
            builder.Property(m => m.QuestionCode).HasMaxLength(20);
            builder.Property(m => m.Title).HasMaxLength(350);
            builder.HasOne(m => m.TechnicianSuggestedSupport)
                .WithMany(m => m.TechnicianRequestSupports)
                .HasForeignKey(m => m.TechnicianSuggestedSupportId);

            //Value Object
            builder.OwnsOne(m => m.Technician);
        }
    }
}
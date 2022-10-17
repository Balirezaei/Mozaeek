using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekCore.Domain;

namespace MozaeekCore.Persistense.EF.EfConfiguration
{
    public class AnnouncementLabelConfiguration : IEntityTypeConfiguration<AnnouncementLabel>
    {
        public void Configure(EntityTypeBuilder<AnnouncementLabel> builder)
        {
            builder.ToTable("AnnouncementLabel");

            builder.HasOne(a => a.Announcement)
                .WithMany(a => a.AnnouncementLabels)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(m => m.AnnouncementId);

            builder.HasOne(a => a.Label)
                .WithMany(a => a.AnnouncementLabels)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(m => m.LabelId);
        }
    }
}
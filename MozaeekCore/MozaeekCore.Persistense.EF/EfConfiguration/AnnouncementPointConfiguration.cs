using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekCore.Domain;

namespace MozaeekCore.Persistense.EF.EfConfiguration
{
    public class AnnouncementPointConfiguration : IEntityTypeConfiguration<AnnouncementPoint>
    {
        public void Configure(EntityTypeBuilder<AnnouncementPoint> builder)
        {
            builder.ToTable("AnnouncementPoint");
            builder.HasKey(e => e.Id);

            builder
                .HasOne(m => m.Announcement)
                .WithMany(m => m.AnnouncementPoints)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(m => m.AnnouncementId);

            builder
                .HasOne(m => m.Point)
                .WithMany(m => m.AnnouncementPoints)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(m => m.PointId);
        }
    }
}
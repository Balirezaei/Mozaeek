using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekCore.Domain;

namespace MozaeekCore.Persistense.EF.EfConfiguration
{
    public class AnnouncementConfiguration : IEntityTypeConfiguration<Announcement>
    {
        public void Configure(EntityTypeBuilder<Announcement> builder)
        {
            builder.ToTable("Announcement");
            builder.HasKey(e => e.Id);
            builder.Property(m => m.Title).HasMaxLength(150);

            builder.Property(m => m.Summary).HasMaxLength(250);

            builder.HasOne(m => m.Request)
                .WithMany(m => m.Announcements)
                .HasForeignKey(m => m.RequestId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
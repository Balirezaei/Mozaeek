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
            builder.HasOne(m => m.RequestTarget)
                .WithMany(m => m.Announcements)
                .HasForeignKey(m => m.RequestTargetId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
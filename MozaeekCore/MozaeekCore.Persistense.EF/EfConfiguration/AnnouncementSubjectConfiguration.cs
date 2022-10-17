using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekCore.Domain;

namespace MozaeekCore.Persistense.EF.EfConfiguration
{
    public class AnnouncementSubjectConfiguration : IEntityTypeConfiguration<AnnouncementSubject>
    {
        public void Configure(EntityTypeBuilder<AnnouncementSubject> builder)
        {
            builder.ToTable("AnnouncementSubject");

            builder.HasOne(a => a.Announcement)
                .WithMany(a => a.AnnouncementSubjects)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(m => m.AnnouncementId);

            builder.HasOne(a => a.Subject)
                .WithMany(a => a.AnnouncementSubjects)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(m => m.SubjectId);
        }
    }
}
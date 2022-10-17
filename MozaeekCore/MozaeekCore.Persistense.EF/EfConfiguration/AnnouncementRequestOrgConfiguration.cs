using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekCore.Domain;

namespace MozaeekCore.Persistense.EF.EfConfiguration
{
    public class AnnouncementRequestOrgConfiguration : IEntityTypeConfiguration<AnnouncementRequestOrg>
    {
        public void Configure(EntityTypeBuilder<AnnouncementRequestOrg> builder)
        {
            builder.ToTable("AnnouncementRequestOrg");

            builder.HasOne(a => a.Announcement)
                .WithMany(a => a.AnnouncementRequestOrgs)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(m => m.AnnouncementId);

            builder.HasOne(a => a.RequestOrg)
                .WithMany(a => a.AnnouncementRequestOrgs)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(m => m.RequestOrgId);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekCore.Domain;

namespace MozaeekCore.Persistense.EF.EfConfiguration
{
    public class RequestTargetRequestOrgConfiguration : IEntityTypeConfiguration<RequestTargetRequestOrg>
    {
        public void Configure(EntityTypeBuilder<RequestTargetRequestOrg> builder)
        {
            builder.ToTable("RequestTargetRequestOrg");

            builder.HasOne(a => a.RequestTarget)
                .WithMany(a => a.RequestTargetRequestOrgs)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(m => m.RequestTargetId);

            builder.HasOne(a => a.RequestOrg)
                .WithMany(a => a.RequestTargetRequestOrgs)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(m => m.RequestOrgId);
        }
    }
}
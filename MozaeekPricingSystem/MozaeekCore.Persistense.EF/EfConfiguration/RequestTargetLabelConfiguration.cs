using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekCore.Domain;

namespace MozaeekCore.Persistense.EF.EfConfiguration
{
    public class RequestTargetLabelConfiguration : IEntityTypeConfiguration<RequestTargetLabel>
    {
        public void Configure(EntityTypeBuilder<RequestTargetLabel> builder)
        {
            builder.ToTable("RequestTargetLabel");

            builder.HasOne(a => a.RequestTarget)
                .WithMany(a => a.RequestTargetLabels)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(m => m.RequestTargetId);

            builder.HasOne(a => a.Label)
                .WithMany(a => a.RequestTargetLabels)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(m => m.LabelId);
        }
    }
}
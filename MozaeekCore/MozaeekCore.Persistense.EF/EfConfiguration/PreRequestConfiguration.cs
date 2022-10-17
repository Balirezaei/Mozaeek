using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekCore.Domain;

namespace MozaeekCore.Persistense.EF.EfConfiguration
{
    public class PreRequestConfiguration: IEntityTypeConfiguration<PreRequest>
    {
        public void Configure(EntityTypeBuilder<PreRequest> builder)
        {
            builder.ToTable("PreRequest");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Title).HasMaxLength(150);
        }
    }
}
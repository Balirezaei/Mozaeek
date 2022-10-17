using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekCore.Domain;

namespace MozaeekCore.Persistense.EF.EfConfiguration
{
    public class RSSConfiguration : IEntityTypeConfiguration<RSS>
    {
        public void Configure(EntityTypeBuilder<RSS> builder)
        {
            builder.ToTable("RSS");

            builder.HasKey(e => e.Id);

            builder.Property(s => s.Url)
                .IsRequired(false).HasMaxLength(150);

            builder.Property(s => s.Source)
                .IsRequired(false).HasMaxLength(50);
        }
    }
}
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekCore.Domain;

namespace MozaeekCore.Persistense.EF.EfConfiguration
{
    public class PointConfiguration : IEntityTypeConfiguration<Point>
    {
        public void Configure(EntityTypeBuilder<Point> builder)
        {
            builder.ToTable("Point");
            builder.HasKey(m => m.Id);

            builder.Property(s => s.Title)
                .IsRequired(false).HasMaxLength(50);

            builder.HasMany(m => m.RequestPoints).WithOne(m => m.Point).HasForeignKey(m => m.PointId);

        }
    }
}
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekCore.Domain;
namespace MozaeekCore.Persistense.EF.EfConfiguration
{
    public class LabelConfiguration : IEntityTypeConfiguration<Label>
    {
        public void Configure(EntityTypeBuilder<Label> builder)
        {
            builder.ToTable("Label");

            builder.HasKey(e => e.Id);

            builder.Property(s => s.Title)
                .IsRequired(false).HasMaxLength(50);
        }
    }
}
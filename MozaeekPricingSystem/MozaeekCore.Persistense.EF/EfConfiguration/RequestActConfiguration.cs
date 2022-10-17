using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekCore.Domain;

namespace MozaeekCore.Persistense.EF.EfConfiguration
{
    public class RequestActConfiguration : IEntityTypeConfiguration<RequestAct>
    {
        public void Configure(EntityTypeBuilder<RequestAct> builder)
        {
            builder.ToTable("RequestAct");
            builder.HasKey(m => m.Id);

            builder.Property(s => s.Title)
                .IsRequired(false).HasMaxLength(80);

            
        }
    }
}
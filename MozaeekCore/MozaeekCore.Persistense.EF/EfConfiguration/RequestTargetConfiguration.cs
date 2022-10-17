using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekCore.Domain;

namespace MozaeekCore.Persistense.EF.EfConfiguration
{
    public class RequestTargetConfiguration : IEntityTypeConfiguration<RequestTarget>
    {
        public void Configure(EntityTypeBuilder<RequestTarget> builder)
        {
            builder.ToTable("RequestTarget");
            builder.HasKey(m => m.Id);

            builder.Property(s => s.Title)
                .IsRequired(false).HasMaxLength(100);

            builder.Property(m => m.Icon).HasMaxLength(30);
        }
    }
}

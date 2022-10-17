using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekCore.Domain;

namespace MozaeekCore.Persistense.EF.EfConfiguration
{
    public class RequestOrgConfiguration : IEntityTypeConfiguration<RequestOrg>
    {
        public void Configure(EntityTypeBuilder<RequestOrg> builder)
        {
            builder.ToTable("RequestOrg");
            builder.HasKey(m => m.Id);

            builder.Property(s => s.Title)
                .IsRequired(false).HasMaxLength(50);

            builder.Property(m => m.Icon).HasMaxLength(30);
        }
    }
}
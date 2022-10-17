using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekCore.Domain.Pricing;

namespace MozaeekCore.Persistense.EF.EfConfiguration
{
    public class PriceUnitConfiguration : IEntityTypeConfiguration<PriceCurrency>
    {
        public void Configure(EntityTypeBuilder<PriceCurrency> builder)
        {
            builder.ToTable("PriceCurrency");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.CurrencyCode).HasMaxLength(10);
            builder.Property(m => m.Unit).HasMaxLength(15);
            builder.HasData(new List<PriceCurrency>()
            {
                new PriceCurrency(  1,  "ریال",  "IRR")
            });
        }
    }
}
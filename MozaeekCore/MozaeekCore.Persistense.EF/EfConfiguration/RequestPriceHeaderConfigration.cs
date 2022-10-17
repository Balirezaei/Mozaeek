using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekCore.Domain.Pricing;

namespace MozaeekCore.Persistense.EF.EfConfiguration
{
    public class RequestPriceHeaderConfigration : IEntityTypeConfiguration<RequestPriceHeader>
    {
        public void Configure(EntityTypeBuilder<RequestPriceHeader> builder)
        {
            builder.ToTable("RequestPriceHeader");
            builder.HasKey(e => e.Id);
            builder.Property(m => m.Title).HasMaxLength(150);
            builder.HasOne(m => m.PriceCurrency)
                .WithMany(m => m.RequestPriceHeaders)
                .HasForeignKey(m => m.PriceCurrencyId);

            builder.HasMany(m => m.PriceDetails)
                .WithOne(m => m.RequestPriceHeader)
                .HasForeignKey(m => m.RequestPriceHeaderId);


        }
    }
}
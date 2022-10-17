using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekCore.Domain.Pricing;

namespace MozaeekCore.Persistense.EF.EfConfiguration
{
    public class SubjectPriceHeaderConfigration : IEntityTypeConfiguration<SubjectPriceHeader>
    {
        public void Configure(EntityTypeBuilder<SubjectPriceHeader> builder)
        {
            builder.ToTable("SubjectPriceHeader");
            builder.HasKey(e => e.Id);
            builder.Property(m => m.Title).HasMaxLength(150);
            builder.HasOne(m => m.PriceCurrency)
                .WithMany(m => m.SubjectPriceHeaders)
                .HasForeignKey(m=>m.PriceCurrencyId);

            builder.HasMany(m => m.PriceDetails)
                .WithOne(m => m.SubjectPriceHeader)
                .HasForeignKey(m => m.SubjectPriceHeaderId);
        }
    }
}
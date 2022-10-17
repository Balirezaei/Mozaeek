using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekCore.Domain;

namespace MozaeekCore.Persistense.EF.EfConfiguration
{
    public class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder.ToTable("Request");
            builder.HasKey(m => m.Id);

            builder.HasOne(m => m.RequestTarget)
                .WithMany(m => m.Requests)
                .HasForeignKey(m => m.RequestTargetId);

            builder.HasOne(m => m.RequestAct)
                .WithMany(m => m.Requests)
                .HasForeignKey(m => m.RequestActId);


            builder.HasMany(m => m.RequestPoints).WithOne(m => m.Request).HasForeignKey(m => m.RequestId);
            //Address value object persisted as owned entity in EF Core 2.0
            builder.OwnsMany(o => o.RequestActions);
            builder.OwnsMany(o => o.RequestQualifications);
            builder.OwnsMany(o => o.RequestDocuments);
            builder.OwnsMany(o => o.RequestNessesities);

        }
    }
}
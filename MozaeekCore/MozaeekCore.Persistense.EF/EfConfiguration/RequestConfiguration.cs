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

            builder.Property(m => m.Description).HasMaxLength(250);

            builder.HasOne(m => m.RequestTarget)
                .WithMany(m => m.Requests)
                .HasForeignKey(m => m.RequestTargetId);

            builder.HasOne(m => m.RequestAct)
                .WithMany(m => m.Requests)
                .HasForeignKey(m => m.RequestActId);


            builder.HasMany(m => m.RequestPoints).WithOne(m => m.Request).HasForeignKey(m => m.RequestId);
            builder.HasMany(m => m.RequestDefiniteRequestOrgs)
                .WithOne(m => m.Request).HasForeignKey(m => m.RequestId);

            builder.HasMany(m => m.RequestRequestOrgs)
                .WithOne(m => m.Request)
                .HasForeignKey(m => m.RequestId);

            //Address value object persisted as owned entity in EF Core 2.0
            builder.OwnsMany(o => o.RequestActions);
            builder.OwnsMany(o => o.RequestQualifications);
            builder.OwnsMany(o => o.RequestNecessities);
            builder.OwnsMany(m => m.RequestTargetConnections);

        }

        public class RequestDefiniteRequestOrgConfiguration : IEntityTypeConfiguration<RequestDefiniteRequestOrg>
        {
            public void Configure(EntityTypeBuilder<RequestDefiniteRequestOrg> builder)
            {
                builder.ToTable("RequestDefiniteRequestOrg");
                builder.HasKey(m=>m.Id);

                builder.HasOne(m => m.Request)
                    .WithMany(m => m.RequestDefiniteRequestOrgs)
                    .HasForeignKey(m => m.RequestId);

                builder.HasOne(m => m.DefiniteRequestOrg)
                    .WithMany(m => m.RequestDefiniteRequestOrgs)
                    .HasForeignKey(m => m.DefiniteRequestOrgId);
            }
        }

        public class RequestRequestOrgConfiguration : IEntityTypeConfiguration<RequestRequestOrg>
        {
            public void Configure(EntityTypeBuilder<RequestRequestOrg> builder)
            {
                builder.ToTable("RequestRequestOrg");
                builder.HasKey(m => m.Id);

                builder.HasOne(m => m.Request)
                    .WithMany(m => m.RequestRequestOrgs)
                    .HasForeignKey(m => m.RequestId);

                builder.HasOne(m => m.RequestOrg)
                    .WithMany(m => m.RequestRequestOrgs)
                    .HasForeignKey(m => m.RequestOrgId);
            }
        }
    }

}
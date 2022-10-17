using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekCore.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekCore.Persistense.EF.EfConfiguration
{

    public class TechnicianDefiniteRequestOrgConfiguration : IEntityTypeConfiguration<TechnicianDefiniteRequestOrg>
    {
        public void Configure(EntityTypeBuilder<TechnicianDefiniteRequestOrg> builder)
        {
            builder.ToTable("TechnicianDefiniteRequestOrg");

            builder.HasOne(a => a.Technician)
                .WithMany(a => a.TechnicianDefiniteRequestOrgs)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(m => m.TechnicianId);

            builder.HasOne(a => a.DefiniteRequestOrg)
                .WithMany(a => a.TechnicianDefiniteRequestOrgs)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(m => m.DefiniteRequestOrgId);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekCore.Domain;

namespace MozaeekCore.Persistense.EF.EfConfiguration
{
    public class TechnicianConfiguration : IEntityTypeConfiguration<Technician>
    {
        public void Configure(EntityTypeBuilder<Technician> builder)
        {
            builder.ToTable("Technician");

            builder.HasKey(m => m.Id);

            builder
                .HasOne(m => m.TechnicianPersonalInfo)
                .WithMany(m => m.Technicians)
                .HasForeignKey(m => m.TechnicianPersonalInfoId);
            
            builder
                .HasOne(m => m.TechnicianContactInfo)
                .WithMany(m => m.Technicians)
                .HasForeignKey(m => m.TechnicianContactInfoId);
            
            builder
                .HasOne(m => m.TechnicianEducationalInfo)
                .WithMany(m => m.Technicians)
                .HasForeignKey(m => m.TechnicianEducationalInformationId);
            
            builder
                .HasMany(m => m.TechnicianAttachments)
                .WithOne(m => m.Technician)
                .HasForeignKey(m => m.TechnicianId);
            
            builder
                .HasMany(m => m.TechnicianAttachments)
                .WithOne(m => m.Technician)
                .HasForeignKey(m => m.TechnicianId);

            builder.HasMany(m => m.TechnicianPoints)
                .WithOne(m => m.Technician)
                .HasForeignKey(m => m.TechnicianId);

            builder
                .HasMany(m => m.TechnicianRequests)
                .WithOne(m => m.Technician)
                .HasForeignKey(m => m.TechnicianId);

            builder
                .HasMany(m => m.TechnicianSubjects)
                .WithOne(m => m.Technician)
                .HasForeignKey(m => m.TechnicianId);
            
            builder
                .Property(m => m.TechnicianType)
                .HasColumnType("TINYINT");
        }
    }
}
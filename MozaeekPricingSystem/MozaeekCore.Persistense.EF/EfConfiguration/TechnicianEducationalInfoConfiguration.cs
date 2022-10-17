using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekCore.Domain;

namespace MozaeekCore.Persistense.EF.EfConfiguration
{
    public class TechnicianEducationalInfoConfiguration : IEntityTypeConfiguration<TechnicianEducationalInfo>
    {
        public void Configure(EntityTypeBuilder<TechnicianEducationalInfo> builder)
        {
            builder.ToTable("TechnicianEducationalInfo");
            builder.HasKey(m => m.Id);

            builder
                .HasOne(m => m.EducationField)
                .WithMany(m => m.TechnicianEducationalInfos)
                .HasForeignKey(m => m.EducationFieldId);


            builder
                .HasOne(m => m.EducationGrade)
                .WithMany(m => m.TechnicianEducationalInfos)
                .HasForeignKey(m => m.EducationGradeId);


        }
    }
}
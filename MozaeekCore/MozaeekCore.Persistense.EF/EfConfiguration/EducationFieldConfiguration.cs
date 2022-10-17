using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekCore.Domain;

namespace MozaeekCore.Persistense.EF.EfConfiguration
{
    public class EducationFieldConfiguration : IEntityTypeConfiguration<EducationField>
    {
        public void Configure(EntityTypeBuilder<EducationField> builder)
        {
            builder.ToTable("EducationField");

            builder.HasOne(m => m.Parent)
                .WithMany(m => m.SubEducationFields)
                .HasForeignKey(m => m.ParentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
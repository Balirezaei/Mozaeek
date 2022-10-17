using Karmizban.Support.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karmizban.Support.EF.EfConfiguration
{
    public class TechnicianRequestSupportAnswerConfiguration : IEntityTypeConfiguration<TechnicianRequestSupportAnswer>
    {
        public void Configure(EntityTypeBuilder<TechnicianRequestSupportAnswer> builder)
        {
            builder.ToTable("TechnicianRequestSupportAnswer");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.AnswerDescription)
                .HasMaxLength(2500);
            builder.HasOne(m => m.TechnicianRequestSupport)
                .WithMany(m => m.TechnicianRequestSupportAnswers)
                .HasForeignKey(m => m.TechnicianRequestSupportId);
        }
    }
}
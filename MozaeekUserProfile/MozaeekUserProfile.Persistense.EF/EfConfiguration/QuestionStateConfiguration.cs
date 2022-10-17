using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekUserProfile.Domain;

namespace MozaeekUserProfile.Persistense.EF.EfConfiguration
{
    public class QuestionStateConfiguration : IEntityTypeConfiguration<QuestionState>
    {
        public void Configure(EntityTypeBuilder<QuestionState> builder)
        {
            builder.ToTable("QuestionState");

            builder.Property(m => m.Description).HasMaxLength(1500);

            builder
                .HasOne(m => m.UserQuestion)
                .WithMany(m => m.QuestionStates);

        }
    }
}
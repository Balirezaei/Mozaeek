using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekUserProfile.Domain;

namespace MozaeekUserProfile.Persistense.EF.EfConfiguration
{
    public class UserQuestionConfiguration : IEntityTypeConfiguration<UserQuestion>
    {
        public void Configure(EntityTypeBuilder<UserQuestion> builder)
        {
            builder.ToTable("UserQuestion");

            builder.HasKey(m => m.Id);

            builder
                .Property(m => m.TextDescription)
                .HasMaxLength(1500);

            builder.Property(m => m.QuestionCodePreFix)
                .HasMaxLength(3);

            builder.Property(m => m.QuestionCodeNo)
                .HasMaxLength(6)
                .HasDefaultValueSql("convert(varchar,(NEXT VALUE FOR QuestionCode) )");

            builder.HasOne(m => m.User)
                .WithMany(m => m.UserQuestions)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            //builder.HasOne(a => a.UserQuestionPrice)
            //    .WithOne(b => b.UserQuestion)
            //    .HasForeignKey<UserQuestion>(m => m.UserQuestionPriceId);
        }
    }
}
using Karmizban.Support.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karmizban.Support.EF.EfConfiguration
{
    public class UserRequestSupportAnswerConfiguration : IEntityTypeConfiguration<UserRequestSupportAnswer>
    {
        public void Configure(EntityTypeBuilder<UserRequestSupportAnswer> builder)
        {
            builder.ToTable("UserRequestSupportAnswer");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.AnswerDescription)
                .HasMaxLength(2500);
            //builder.HasOne(m => m.UserRequestSupport)
            //    .WithMany(m => m.UserRequestSupportAnswers)
            //    .HasForeignKey(m => m.UserRequestSupportId);
        }
    }
}
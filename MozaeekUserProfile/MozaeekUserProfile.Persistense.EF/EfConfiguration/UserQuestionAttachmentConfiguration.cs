using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using MozaeekUserProfile.Domain;

namespace MozaeekUserProfile.Persistense.EF.EfConfiguration
{
    public class UserQuestionAttachmentConfiguration: IEntityTypeConfiguration<UserQuestionAttachment>
    {
        public void Configure(EntityTypeBuilder<UserQuestionAttachment> builder)
        {
            builder
                .Property(m => m.FileHttpAddress)
                .HasMaxLength(100);

            builder.HasOne(m => m.UserQuestion)
                .WithMany(m => m.UserQuestionAttachments)
                .HasForeignKey(m => m.UserQuestionId);


        }
    }
}
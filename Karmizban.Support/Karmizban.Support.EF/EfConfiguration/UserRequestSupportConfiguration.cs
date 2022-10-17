using Karmizban.Support.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karmizban.Support.EF.EfConfiguration
{
    public class UserRequestSupportConfiguration:IEntityTypeConfiguration<UserRequestSupport>
    {
        public void Configure(EntityTypeBuilder<UserRequestSupport> builder)
        {
            builder.ToTable("UserRequestSupport");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Description)
                .HasMaxLength(2500);
            builder.Property(m => m.QuestionCode).HasMaxLength(20);
            builder.Property(m => m.Title).HasMaxLength(350);
            builder.HasOne(m => m.UserSuggestedSupport)
                .WithMany(m => m.UserRequestSupports)
                .HasForeignKey(m => m.UserSuggestedSupportId);

            builder.HasOne(m => m.UserRequestSupportAnswer);

            
            //Value Object
            builder.OwnsOne(m => m.User);
        }
    }
}
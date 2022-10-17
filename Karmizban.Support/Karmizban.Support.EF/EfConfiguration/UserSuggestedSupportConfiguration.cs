using Karmizban.Support.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karmizban.Support.EF.EfConfiguration
{
    public class UserSuggestedSupportConfiguration : IEntityTypeConfiguration<UserSuggestedSupport>
    {
        public void Configure(EntityTypeBuilder<UserSuggestedSupport> builder)
        {
            builder.ToTable("UserSuggestedSupport");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Title).HasMaxLength(350);
            builder.Property(m => m.Description)
                .HasMaxLength(2500);
        }
    }
}
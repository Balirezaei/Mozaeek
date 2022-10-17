using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekUserProfile.Domain;

namespace MozaeekUserProfile.Persistense.EF.EfConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(m => m.Id);

            builder.Property(s => s.PhoneNumber)
                .IsRequired(false).HasMaxLength(11);

            // builder.HasData(new List<Subject>()
            // {
            //     first,second,second2,third
            // });
        }
    }
}
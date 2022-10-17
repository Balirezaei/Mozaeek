using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekCore.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using MozaeekCore.Enum;

namespace MozaeekCore.Persistense.EF.EfConfiguration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRole");

            builder.HasKey(e => e.Id);            
        }
    }
    public class UserConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(e => e.Id);
            builder.Property(x => x.EMail)
                .HasMaxLength(200);
            builder.Property(x => x.FirstName)
                .HasMaxLength(200);
            builder.Property(x => x.LastName)
                .HasMaxLength(200);
            builder.Property(x => x.NationalCode)
                .HasMaxLength(12);
            builder.Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.Salt)
                .HasMaxLength(200);
            builder.Property(x => x.UserName)
                .IsRequired()
                .HasMaxLength(20);

            
        }
    }
}

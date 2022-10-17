using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekCore.Domain;

namespace MozaeekCore.Persistense.EF.EfConfiguration
{
    public class RequestTargetSubjectConfiguration : IEntityTypeConfiguration<RequestTargetSubject>
    {
        public void Configure(EntityTypeBuilder<RequestTargetSubject> builder)
        {
            builder.ToTable("RequestTargetSubject");

            builder.HasOne(a => a.RequestTarget)
                .WithMany(a => a.RequestTargetSubjects)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(m => m.RequestTargetId);

            builder.HasOne(a => a.Subject)
                .WithMany(a => a.RequestTargetSubjects)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(m => m.SubjectId);
        }
    }
}
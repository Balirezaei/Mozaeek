using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekCore.Domain;

namespace MozaeekCore.Persistense.EF.EfConfiguration
{
    public class FileConfiguration:IEntityTypeConfiguration<MosaikFile>
    {
        public void Configure(EntityTypeBuilder<MosaikFile> builder)
        {
            builder.ToTable("File");
            builder.HasKey(e => e.Id);
            builder.Property(m => m.Name).HasMaxLength(150);
            builder.Property(m => m.Extension).HasMaxLength(20);
            builder.Property(m => m.Path).HasMaxLength(400);
            builder.Property(m => m.Type).HasMaxLength(20);
            builder.Property(m => m.Url).HasMaxLength(400);
        }
    
    }
}

using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseCatalog.Persistence.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tags", "Common");
            builder.Property(s => s.Name).HasColumnName("TagName");
            builder.Property(s => s.Description).HasColumnName("TagDescription");
            builder.Property(s => s.GroupName).HasColumnName("TagGroupName");
        }
    }
}
using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseCatalog.Persistence.Configurations
{
    public class CourseLevelConfiguration : IEntityTypeConfiguration<CourseLevel>
    {
        public void Configure(EntityTypeBuilder<CourseLevel> builder)
        {
            builder.ToTable("CourseLevels", "Common");
            builder.Property(s => s.Name).HasColumnName("CourseLevelName");
            builder.Property(s => s.Description).HasColumnName("CourseLevelDescription");

        }
    }
}
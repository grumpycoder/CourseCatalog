using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseCatalog.Persistence.Configurations
{
    public class CourseViewConfiguration : IEntityTypeConfiguration<CourseView>
    {
        public void Configure(EntityTypeBuilder<CourseView> builder)
        {
            builder.ToView("v_Courses", "Common");
            builder.HasKey(s => s.CourseId);
            builder.Property(s => s.Name).HasColumnName("CourseName");
            builder.Property(s => s.Description).HasColumnName("CourseDescription");

        }
    }
}
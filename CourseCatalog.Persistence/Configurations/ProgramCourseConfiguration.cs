using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseCatalog.Persistence.Configurations
{
    public class ProgramCourseConfiguration : IEntityTypeConfiguration<ProgramCourse>
    {
        public void Configure(EntityTypeBuilder<ProgramCourse> builder)
        {
            builder.ToTable("ProgramCourses", "CareerTech");
        }
    }

    public class ProgramDraftConfiguration : IEntityTypeConfiguration<ProgramDraft>
    {
        public void Configure(EntityTypeBuilder<ProgramDraft> builder)
        {
            builder.ToTable("ProgramCourses", "Draft");
        }
    }
}
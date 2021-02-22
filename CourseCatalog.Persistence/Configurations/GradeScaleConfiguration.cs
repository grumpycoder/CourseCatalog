using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseCatalog.Persistence.Configurations
{
    public class GradeScaleConfiguration : IEntityTypeConfiguration<GradeScale>
    {
        public void Configure(EntityTypeBuilder<GradeScale> builder)
        {
            builder.ToTable("GradeScales", "Common");
            builder.Property(s => s.Configuration).HasColumnName("Configuration");
        }
    }
}
using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseCatalog.Persistence.Configurations
{
    public class PerformanceLogDetailConfiguration : IEntityTypeConfiguration<PerformanceLogDetail>
    {
        public void Configure(EntityTypeBuilder<PerformanceLogDetail> builder)
        {
            builder.ToTable("Perf", "Log");
            builder.Property(s => s.Id).HasColumnName("Id");
        }
    }
}
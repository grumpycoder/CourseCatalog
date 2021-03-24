using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseCatalog.Persistence.Configurations
{
    public class ErrorLogDetailConfiguration : IEntityTypeConfiguration<ErrorLogDetail>
    {
        public void Configure(EntityTypeBuilder<ErrorLogDetail> builder)
        {
            builder.ToTable("Error", "Log");
            builder.HasKey(s => s.LogId);
            builder.Property(s => s.LogId).HasColumnName("LogId");
        }
    }
}

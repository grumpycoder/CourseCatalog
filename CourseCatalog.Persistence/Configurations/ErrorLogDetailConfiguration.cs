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
            builder.Property(s => s.Id).HasColumnName("Id");
        }
    }
}

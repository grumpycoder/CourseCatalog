using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseCatalog.Persistence.Configurations
{
    public class ProgramConfiguration : IEntityTypeConfiguration<Program>
    {
        public void Configure(EntityTypeBuilder<Program> builder)
        {
            builder.ToTable("Programs", "CareerTech");
            builder.Property(s => s.Name).HasColumnName("ProgramName");
            builder.Property(s => s.Description).HasColumnName("ProgramDescription");

        }
    }
}
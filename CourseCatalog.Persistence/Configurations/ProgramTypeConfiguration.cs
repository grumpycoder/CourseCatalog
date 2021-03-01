using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseCatalog.Persistence.Configurations
{
    public class ProgramTypeConfiguration : IEntityTypeConfiguration<ProgramType>
    {
        public void Configure(EntityTypeBuilder<ProgramType> builder)
        {
            builder.ToTable("ProgramTypes", "CareerTech");
            builder.Property(s => s.Name).HasColumnName("ProgramTypeName");
            builder.Property(s => s.Description).HasColumnName("ProgramTypeDescription");

        }
    }
}
using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseCatalog.Persistence.Configurations
{
    public class CourseEndorsementConfiguration : IEntityTypeConfiguration<CourseEndorsement>
    {
        public void Configure(EntityTypeBuilder<CourseEndorsement> builder)
        {
            builder.ToTable("CourseEndorsements", "Common");
            builder.Property(s => s.EndorsementId).HasColumnName("EndorseId");
            //builder.HasKey(s => s.CourseEndorsementId); 
        }
    }

    public class DraftEndorsementConfiguration : IEntityTypeConfiguration<DraftEndorsement>
    {
        public void Configure(EntityTypeBuilder<DraftEndorsement> builder)
        {
            builder.ToTable("CourseEndorsements", "Draft");
            builder.Property(s => s.EndorsementId).HasColumnName("EndorseId");
            //builder.HasKey(s => s.CourseEndorsementId); 
        }
    }
}
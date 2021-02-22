using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseCatalog.Persistence.Configurations
{
    public class CourseDeliveryTypeConfiguration : IEntityTypeConfiguration<CourseDeliveryType>
    {
        public void Configure(EntityTypeBuilder<CourseDeliveryType> builder)
        {
            builder.ToTable("CourseDeliveryTypes", "Common");
        }
    }

    public class DraftDeliveryTypeConfiguration : IEntityTypeConfiguration<DraftDeliveryType>
    {
        public void Configure(EntityTypeBuilder<DraftDeliveryType> builder)
        {
            builder.ToTable("CourseDeliveryTypes", "Draft");
        }
    }
}
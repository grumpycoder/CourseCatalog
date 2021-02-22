using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseCatalog.Persistence.Configurations
{
    public class DeliveryTypeConfiguration : IEntityTypeConfiguration<DeliveryType>
    {
        public void Configure(EntityTypeBuilder<DeliveryType> builder)
        {
            builder.ToTable("DeliveryTypes", "Common");
            builder.Property(s => s.Name).HasColumnName("DeliveryTypeName");
            builder.Property(s => s.Description).HasColumnName("DeliveryTypeDescription");
        }
    }
}
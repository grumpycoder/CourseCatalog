using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseCatalog.Persistence.Configurations
{
    public class CipConfiguration : IEntityTypeConfiguration<Cip>
    {
        public void Configure(EntityTypeBuilder<Cip> builder)
        {
            builder.ToTable("v_CipCodes", "CareerTech");
            builder.HasKey(c => c.CipCode);
            builder.Property(s => s.CipId).HasColumnName("CipCodeId");
        }
    }
}
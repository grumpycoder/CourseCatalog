using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseCatalog.Persistence.Configurations
{
    public class CertholderConfiguration : IEntityTypeConfiguration<Certholder>
    {
        public void Configure(EntityTypeBuilder<Certholder> builder)
        {
            builder.ToView("v_Certholders", "Common");
            builder.HasKey(c => c.CertholderId);
        }
    }
}
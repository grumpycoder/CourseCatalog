using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseCatalog.Persistence.Configurations
{
    public class CertificateConfiguration : IEntityTypeConfiguration<Certificate>
    {
        public void Configure(EntityTypeBuilder<Certificate> builder)
        {
            builder.ToView("v_Certificates", "Common");
            builder.HasNoKey();
            //builder.HasKey(c => c.CertholderId);
            builder.Property(c => c.EndorsementId).HasColumnName("EndorseId");
        }
    }
}
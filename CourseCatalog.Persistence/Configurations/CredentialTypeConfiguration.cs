using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseCatalog.Persistence.Configurations
{
    public class CredentialTypeConfiguration : IEntityTypeConfiguration<CredentialType>
    {
        public void Configure(EntityTypeBuilder<CredentialType> builder)
        {
            builder.ToTable("CredentialTypes", "CareerTech");
            builder.Property(s => s.CredentialTypeCode).HasColumnName("CredentialTypeCode");
            builder.Property(s => s.Name).HasColumnName("CredentialTypeName");
            builder.Property(s => s.Description).HasColumnName("CredentialTypeDescription");

        }
    }
}
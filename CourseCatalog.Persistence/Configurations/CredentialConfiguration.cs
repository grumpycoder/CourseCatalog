using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseCatalog.Persistence.Configurations
{
    public class CredentialConfiguration : IEntityTypeConfiguration<Credential>
    {
        public void Configure(EntityTypeBuilder<Credential> builder)
        {
            builder.ToTable("Credentials", "CareerTech");
            builder.Property(s => s.Name).HasColumnName("CredentialName");
            builder.Property(s => s.Description).HasColumnName("CredentialDescription");
            builder.Property(s => s.CredentialCode).HasColumnName("CredentialCode");
            builder.Property(s => s.IsReimbursable).HasColumnName("Reimbursable");
        }
    }
}
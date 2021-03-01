using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseCatalog.Persistence.Configurations
{
    public class ProgramCredentialConfiguration : IEntityTypeConfiguration<ProgramCredential>
    {
        public void Configure(EntityTypeBuilder<ProgramCredential> builder)
        {
            builder.ToTable("ProgramCredentials", "CareerTech");
        }
    }
}
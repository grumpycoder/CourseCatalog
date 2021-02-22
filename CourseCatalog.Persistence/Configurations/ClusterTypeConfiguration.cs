using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseCatalog.Persistence.Configurations
{
    public class ClusterTypeConfiguration : IEntityTypeConfiguration<ClusterType>
    {
        public void Configure(EntityTypeBuilder<ClusterType> builder)
        {
            builder.ToTable("ClusterTypes", "CareerTech");
            builder.Property(s => s.Name).HasColumnName("ClusterTypeName");
        }
    }
}
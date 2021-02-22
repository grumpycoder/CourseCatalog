using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseCatalog.Persistence.Configurations
{
    public class ClusterConfiguration : IEntityTypeConfiguration<Cluster>
    {
        public void Configure(EntityTypeBuilder<Cluster> builder)
        {
            builder.ToTable("Clusters", "CareerTech");
            builder.Property(s => s.Name).HasColumnName("ClusterName");
            builder.Property(s => s.Description).HasColumnName("ClusterDescription");
        }
    }
}
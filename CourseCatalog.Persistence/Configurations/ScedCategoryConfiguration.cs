using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseCatalog.Persistence.Configurations
{
    public class ScedCategoryConfiguration : IEntityTypeConfiguration<ScedCategory>
    {
        public void Configure(EntityTypeBuilder<ScedCategory> builder)
        {
            builder.ToView("v_ScedCategories", "Common");
            builder.Property(s => s.Code).HasColumnName("CategoryCode");
            builder.Property(s => s.Name).HasColumnName("Category");
            builder.Property(s => s.Identifier).HasColumnName("Identifier");
        }
    }
}
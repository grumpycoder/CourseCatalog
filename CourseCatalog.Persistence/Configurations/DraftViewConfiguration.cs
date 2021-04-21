using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseCatalog.Persistence.Configurations
{
    public class DraftViewConfiguration : IEntityTypeConfiguration<DraftView>
    {
        public void Configure(EntityTypeBuilder<DraftView> builder)
        {
            builder.ToView("v_Drafts", "Draft");
            builder.HasKey(s => s.DraftId);
            builder.Property(s => s.Name).HasColumnName("CourseName");
            builder.Property(s => s.Description).HasColumnName("CourseDescription");
        }
    }
}
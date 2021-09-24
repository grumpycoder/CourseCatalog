using System.Collections.Generic;
using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace CourseCatalog.Persistence.Configurations
{
    public class CourseViewConfiguration : IEntityTypeConfiguration<CourseView>
    {
        public void Configure(EntityTypeBuilder<CourseView> builder)
        {
            builder.ToView("v_Courses", "Common");
            builder.HasKey(s => s.CourseId);
            builder.Property(s => s.Name).HasColumnName("CourseName");
            builder.Property(s => s.Description).HasColumnName("CourseDescription");

            builder.Property(s => s.Endorsements)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<List<string>>(v)
                );

            builder.Property(s => s.CreditTypes)
        .HasConversion(
            v => JsonConvert.SerializeObject(v),
            v => JsonConvert.DeserializeObject<List<string>>(v)
        );

        }
    }
}
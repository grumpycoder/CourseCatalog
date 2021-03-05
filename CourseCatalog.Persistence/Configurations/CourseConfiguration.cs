using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CourseCatalog.Persistence.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses", "Common");
            builder.Property(s => s.Name).HasColumnName("CourseName");
            builder.Property(s => s.Description).HasColumnName("CourseDescription");

            builder.Property(s => s.CreditTypes)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<List<string>>(v)
                );

            builder.Property(s => s.Tags)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<List<string>>(v)
                );

            builder.Property(e => e.Status).HasConversion<string>();

        }
    }
}

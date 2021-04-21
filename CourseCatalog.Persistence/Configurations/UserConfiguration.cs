using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseCatalog.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "Security");
            builder.Property(s => s.Id).HasColumnName("UserId");
            builder.Property(s => s.FullName).HasColumnName("FullName");
            builder.Property(s => s.Username).HasColumnName("Username");
        }
    }

    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Groups", "Security");
            builder.Property(s => s.Id).HasColumnName("GroupId");
            builder.Property(s => s.Name).HasColumnName("GroupName");
        }
    }

    public class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder.ToTable("GroupUsers", "Security");
            builder.Property(s => s.Id).HasColumnName("GroupUserId");
        }
    }
}
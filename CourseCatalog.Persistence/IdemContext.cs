using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseCatalog.Persistence
{
    public class IdemContext : DbContext
    {

        public DbSet<User> Users { get; set; }

        public IdemContext(ILoggedInUserService loggedInUserService)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var _connectionString = @"Data Source=alsdedevsqli1\i1;initial catalog=Idem;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;MultipleActiveResultSets=true;Application Name=Courses";
            optionsBuilder
                .UseSqlServer(_connectionString)
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new IdemUserConfiguration());
        }
    }

    public class IdemUserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Identities", "Idem");
            builder.Property(s => s.Id).HasColumnName("IdentityId");
            builder.Property(s => s.IdentityGuid).HasColumnName("IdentityGuid");
            builder.Property(s => s.EmailAddress).HasColumnName("EmailAddress");
            builder.Property(s => s.FullName).HasColumnName("PrintName");
            builder.Property(s => s.Username).HasColumnName("EmailAddress");
            //builder.Ignore(s => s.UserGroups)
            ;
        }
    }
}
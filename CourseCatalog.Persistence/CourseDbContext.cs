using CourseCatalog.Domain.Common;
using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.Persistence
{
    public class CourseDbContext : DbContext
    {

        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseView> CoursesView { get; set; }
        public DbSet<DraftView> DraftsView { get; set; }
        public DbSet<Draft> Drafts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<Cluster> Clusters { get; set; }
        public DbSet<Credential> Credentials { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var _connectionString = "Data Source=.;initial catalog=Courses;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;MultipleActiveResultSets=true;Application Name=Courses";
            optionsBuilder
                .UseSqlServer(_connectionString)
                .EnableSensitiveDataLogging()
                //.UseLazyLoadingProxies(_useProxyLoading)
                ;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            //TODO: add user instance to ModifyUser
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.ModifyUser = "system";
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifyUser = "system";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

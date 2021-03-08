using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Common;
using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.Persistence
{
    public class CourseDbContext : DbContext
    {
        private readonly ILoggedInUserService _loggedInUserService;

        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseView> CoursesView { get; set; }
        public DbSet<DraftView> DraftsView { get; set; }
        public DbSet<Draft> Drafts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<Cluster> Clusters { get; set; }
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ErrorLogDetail> ErrorLogs { get; set; }
        public DbSet<PerformanceLogDetail> PerformanceLogs { get; set; }

        public CourseDbContext(ILoggedInUserService loggedInUserService)
        {
            _loggedInUserService = loggedInUserService;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var _connectionString = ConfigurationManager.ConnectionStrings["CourseContext"].ConnectionString;
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
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.ModifyUser = _loggedInUserService.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifyUser = _loggedInUserService.UserId;
                        break;
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

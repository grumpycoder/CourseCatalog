using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseCatalog.Persistence
{
    public class CourseDbContext : DbContext
    {

        public DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var _connectionString = "Data Source=.;initial catalog=Courses;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;MultipleActiveResultSets=true;Application Name=Courses";
            optionsBuilder
                .UseSqlServer(_connectionString)
                .EnableSensitiveDataLogging()
                //.UseLazyLoadingProxies(_useProxyLoading)
                ;
        }
    }
}

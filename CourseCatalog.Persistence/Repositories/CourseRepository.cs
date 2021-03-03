using System.Linq;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CourseCatalog.Persistence.Repositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(CourseDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> GetActiveCourseCount()
        {
            var count =  await _dbContext.CoursesView.CountAsync(c => c.IsRetired == false);
            return count; 
        }

        public async Task<Course> GetCourseByCourseNumber(string courseNumber)
        {
            var course = await _dbContext.Courses.FirstOrDefaultAsync(c => c.CourseNumber == courseNumber);

            return course;
        }

        public async Task<Course> GetCourseByIdWithDetails(int courseId)
        {
            var course = await _dbContext.Courses
                .Include(c => c.CourseLevel)
                .Include(c => c.Subject)
                .Include(c => c.HighGrade)
                .Include(c => c.LowGrade)
                .Include(c => c.GradeScale)
                .Include(c => c.ScedCategory)
                .Include(c => c.DeliveryTypes).ThenInclude(d => d.DeliveryType)
                .Include(c => c.Endorsements).ThenInclude(e => e.Endorsement)
                .Include(c => c.Programs).ThenInclude(e => e.Program)
                .FirstOrDefaultAsync(x => x.CourseId == courseId);

            return course;
        }
    }
}

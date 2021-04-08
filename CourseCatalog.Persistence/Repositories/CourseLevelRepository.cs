using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CourseCatalog.Persistence.Repositories
{
    public class CourseLevelRepository : BaseRepository<CourseLevel>, ICourseLevelRepository
    {
        public CourseLevelRepository(CourseDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<CourseLevel> GetCourseLevelByCourseLevelCode(string courseLevelCode)
        {
            return await _dbContext.CourseLevels.FirstOrDefaultAsync(c => c.CourseLevelCode == courseLevelCode);
        }

        public async Task<bool> HasCourses(int courseLevelId)
        {
            return await _dbContext.Courses.AnyAsync(c => c.CourseLevelId == courseLevelId);
        }
    }
}

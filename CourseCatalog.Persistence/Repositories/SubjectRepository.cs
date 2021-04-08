using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CourseCatalog.Persistence.Repositories
{
    public class SubjectRepository : BaseRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(CourseDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Subject> GetSubjectBySubjectCode(string subjectCode)
        {
            return await _dbContext.Subjects.FirstOrDefaultAsync(c => c.SubjectCode == subjectCode);
        }

        public async Task<bool> HasCourses(int subjectId)
        {
            return await _dbContext.Courses.AnyAsync(c => c.SubjectId == subjectId);
        }
    }
}

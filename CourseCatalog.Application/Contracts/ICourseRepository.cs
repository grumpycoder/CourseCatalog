using CourseCatalog.Domain.Entities;
using System.Threading.Tasks;

namespace CourseCatalog.Application.Contracts
{
    public interface ICourseRepository : IAsyncRepository<Course>
    {
        Task<Course> GetCourseWithDetails(int courseId);
        Task<Course> GetCourseByCourseNumber(string courseNumber);
    }
}

using System.Threading.Tasks;
using CourseCatalog.Domain.Entities;

namespace CourseCatalog.Application.Contracts
{
    public interface ICourseLevelRepository : IAsyncRepository<CourseLevel>
    {
        Task<CourseLevel> GetCourseLevelByCourseLevelCode(string courseLevelCode);
        Task<bool> HasCourses(int courseLevelId);
    }
}
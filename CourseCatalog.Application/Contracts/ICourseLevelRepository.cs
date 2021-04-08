using CourseCatalog.Domain.Entities;
using System.Threading.Tasks;

namespace CourseCatalog.Application.Contracts
{
    public interface ICourseLevelRepository : IAsyncRepository<CourseLevel>
    {
        Task<CourseLevel> GetCourseLevelByCourseLevelCode(string courseLevelCode);
        Task<bool> HasCourses(int courseLevelId);
    }
}
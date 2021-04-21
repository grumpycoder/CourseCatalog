using System.Collections.Generic;
using System.Threading.Tasks;
using CourseCatalog.Domain.Entities;

namespace CourseCatalog.Application.Contracts
{
    public interface ICourseRepository : IAsyncRepository<Course>
    {
        Task<Course> GetCourseByIdWithDetails(int courseId);
        Task<Course> GetCourseByCourseNumber(string courseNumber);
        Task<int> GetActiveCourseCount();
        Task<List<Course>> GetCoursesByEndorseId(int endorseId);
        Task<List<Course>> GetCoursesByCertholderId(int certholderId);
    }
}
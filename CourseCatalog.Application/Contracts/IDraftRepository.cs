using System.Threading.Tasks;
using CourseCatalog.Domain.Entities;

namespace CourseCatalog.Application.Contracts
{
    public interface IDraftRepository : IAsyncRepository<Draft>
    {
        Task<Draft> GetDraftWithDetails(int courseId);
        Task<Draft> GetDraftByCourseNumber(string courseNumber);
    }
}
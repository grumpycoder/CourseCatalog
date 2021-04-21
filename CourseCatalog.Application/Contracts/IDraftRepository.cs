using System.Threading.Tasks;
using CourseCatalog.Domain.Entities;

namespace CourseCatalog.Application.Contracts
{
    public interface IDraftRepository : IAsyncRepository<Draft>
    {
        Task<Draft> GetDraftByIdWithDetails(int draftId);
        Task<Draft> GetDraftByCourseNumber(string courseNumber);
        Task<int> GetDraftCount();
    }
}
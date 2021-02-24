using CourseCatalog.Domain.Entities;
using System.Threading.Tasks;

namespace CourseCatalog.Application.Contracts
{
    public interface IDraftRepository : IAsyncRepository<Draft>
    {
        Task<Draft> GetDraftByIdWithDetails(int draftId);
        Task<Draft> GetDraftByCourseNumber(string courseNumber);
    }
}
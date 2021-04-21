using System.Threading.Tasks;
using CourseCatalog.Domain.Entities;

namespace CourseCatalog.Application.Contracts
{
    public interface ISubjectRepository : IAsyncRepository<Subject>
    {
        Task<Subject> GetSubjectBySubjectCode(string subjectCode);
        Task<bool> HasCourses(int subjectId);
    }
}
using CourseCatalog.Domain.Entities;
using System.Threading.Tasks;

namespace CourseCatalog.Application.Contracts
{
    public interface ISubjectRepository : IAsyncRepository<Subject>
    {
        Task<Subject> GetSubjectBySubjectCode(string subjectCode);
        Task<bool> HasCourses(int subjectId);
    }
}
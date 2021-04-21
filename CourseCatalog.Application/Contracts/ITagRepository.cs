using System.Collections.Generic;
using System.Threading.Tasks;
using CourseCatalog.Domain.Entities;

namespace CourseCatalog.Application.Contracts
{
    public interface ITagRepository : IAsyncRepository<Tag>
    {
        Task<List<Tag>> GetCreditTypeTags();
        Task<List<Tag>> GetGeneralTags();
        Task<Tag> GetCreditTypeByName(string name);
        Task<bool> HasCourses(string name);
    }
}
using CourseCatalog.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseCatalog.Application.Contracts
{
    public interface ITagRepository : IAsyncRepository<Tag>
    {
        Task<List<Tag>> GetCreditTypeTags();
        Task<List<Tag>> GetGeneralTags();
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using CourseCatalog.Domain.Entities;

namespace CourseCatalog.Application.Contracts
{
    public interface IGroupRepository : IAsyncRepository<Group>
    {
        Task<Group> GetGroupByIdWithUsers(int groupId);
        Task<List<Group>> GetGroupsWithUsers();
    }
}
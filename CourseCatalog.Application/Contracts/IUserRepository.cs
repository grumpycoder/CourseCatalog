using CourseCatalog.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace CourseCatalog.Application.Contracts
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<User> GetByIdAsync(Guid userId);
        Task<User> GetUserByIdWithDetails(Guid userId);
    }
}

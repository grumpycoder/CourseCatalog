using System;
using System.Threading.Tasks;
using CourseCatalog.Domain.Entities;

namespace CourseCatalog.Application.Contracts
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<User> GetByIdentityGuidAsync(Guid userId);
        Task<User> GetUserByIdWithDetails(Guid userId);
    }
}
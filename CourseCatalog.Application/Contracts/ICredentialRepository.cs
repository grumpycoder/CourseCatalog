using System.Collections.Generic;
using System.Threading.Tasks;
using CourseCatalog.Domain.Entities;

namespace CourseCatalog.Application.Contracts
{
    public interface ICredentialRepository : IAsyncRepository<Credential>
    {
        Task<List<Credential>> GetCredentialsWithDetails();
        Task<Credential> GetCredentialByIdWithDetails(int id);
        Task<Credential> GetCredentialByCredentialCode(string credentialCode);
    }
}
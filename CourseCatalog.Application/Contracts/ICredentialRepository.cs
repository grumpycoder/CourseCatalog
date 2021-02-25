using CourseCatalog.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseCatalog.Application.Contracts
{
    public interface ICredentialRepository : IAsyncRepository<Credential>
    {
        Task<List<Credential>> GetCredentialsWithDetails();
        Task<Credential> GetCredentialWithDetails(int id);
        Task<Credential> GetCredentialByCredentialCode(string credentialCode);
    }
}
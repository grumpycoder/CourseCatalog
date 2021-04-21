using System;

namespace CourseCatalog.Application.Contracts
{
    public interface ILoggedInUserService
    {
        string UserId { get; }
        Guid IdentityGuid { get; }
    }
}
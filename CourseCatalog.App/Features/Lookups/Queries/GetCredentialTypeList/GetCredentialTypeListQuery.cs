using System.Collections.Generic;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Lookups.Queries.GetCredentialTypeList
{
    public class GetCredentialTypeListQuery : IRequest<List<CredentialType>>
    {
    }
}
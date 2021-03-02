using CourseCatalog.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace CourseCatalog.App.Features.Lookups.Queries.GetCredentialTypeList
{
    public class GetCredentialTypeListQuery : IRequest<List<CredentialType>>
    {

    }
}

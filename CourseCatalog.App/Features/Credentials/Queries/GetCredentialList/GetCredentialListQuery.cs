using System.Collections.Generic;
using MediatR;

namespace CourseCatalog.App.Features.Credentials.Queries.GetCredentialList
{
    public class GetCredentialListQuery : IRequest<List<CredentialListDto>>
    {
    }
}
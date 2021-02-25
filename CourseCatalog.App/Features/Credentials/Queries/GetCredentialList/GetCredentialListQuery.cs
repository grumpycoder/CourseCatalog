using MediatR;
using System.Collections.Generic;

namespace CourseCatalog.App.Features.Credentials.Queries.GetCredentialList
{
    public class GetCredentialListQuery : IRequest<List<CredentialListDto>>
    {

    }
}

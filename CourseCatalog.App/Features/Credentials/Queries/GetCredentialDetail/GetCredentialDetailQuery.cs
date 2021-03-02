using MediatR;

namespace CourseCatalog.App.Features.Credentials.Queries.GetCredentialDetail
{
    public class GetCredentialDetailQuery : IRequest<CredentialDetailDto>
    {
        public int CredentialId { get; set; }

        public GetCredentialDetailQuery(int credentialId)
        {
            CredentialId = credentialId;
        }
    }
}

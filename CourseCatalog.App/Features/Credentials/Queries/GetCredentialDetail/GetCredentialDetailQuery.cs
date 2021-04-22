using MediatR;

namespace CourseCatalog.App.Features.Credentials.Queries.GetCredentialDetail
{
    public class GetCredentialDetailQuery : IRequest<CredentialDetailDto>
    {
        public GetCredentialDetailQuery(int credentialId)
        {
            CredentialId = credentialId;
        }

        public int CredentialId { get; set; }
    }
}
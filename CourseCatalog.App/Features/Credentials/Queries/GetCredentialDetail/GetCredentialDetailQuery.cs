using MediatR;

namespace MyDemo.Api.Features.Credentials.Queries.GetCredentialDetail
{
    public class GetCredentialDetailQuery : IRequest<CredentialDetailVm>
    {
        public int CredentialId { get; set; }
    }
}

using MediatR;

namespace MyDemo.Api.Features.Credentials.Commands.UpdateCredential
{
    public class UpdateCredentialCommand : IRequest
    {
        public int CredentialId { get; set; }
        public string CredentialCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool? IsReimbursable { get; set; }

        public int? BeginYear { get; set; }
        public int? EndYear { get; set; }

        public int CredentialTypeId { get; set; }
    }
}

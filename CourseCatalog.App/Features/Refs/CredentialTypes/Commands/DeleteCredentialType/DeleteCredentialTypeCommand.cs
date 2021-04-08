using MediatR;

namespace CourseCatalog.App.Features.Refs.CredentialTypes.Commands.DeleteCredentialType
{
    public class DeleteCredentialTypeCommand : IRequest
    {
        public int CredentialTypeId { get; set; }

        public DeleteCredentialTypeCommand(int clusterTypeId)
        {
            CredentialTypeId = clusterTypeId;
        }
    }
}

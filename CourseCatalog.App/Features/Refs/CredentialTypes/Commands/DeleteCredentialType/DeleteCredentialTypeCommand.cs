using MediatR;

namespace CourseCatalog.App.Features.Refs.CredentialTypes.Commands.DeleteCredentialType
{
    public class DeleteCredentialTypeCommand : IRequest
    {
        public DeleteCredentialTypeCommand(int clusterTypeId)
        {
            CredentialTypeId = clusterTypeId;
        }

        public int CredentialTypeId { get; set; }
    }
}
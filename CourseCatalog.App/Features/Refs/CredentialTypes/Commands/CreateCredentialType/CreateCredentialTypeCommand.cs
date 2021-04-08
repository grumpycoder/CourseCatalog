using MediatR;

namespace CourseCatalog.App.Features.Refs.CredentialTypes.Commands.CreateCredentialType
{
    public class CreateCredentialTypeCommand : IRequest<int>
    {
        public int CredentialTypeId { get; set; }
        public string CredentialTypeCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

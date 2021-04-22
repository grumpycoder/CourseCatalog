using MediatR;

namespace CourseCatalog.App.Features.Credentials.Commands.DeleteCredentialProgram
{
    public class DeleteCredentialProgramCommand : IRequest
    {
        public DeleteCredentialProgramCommand(int programId, int credentialId)
        {
            ProgramId = programId;
            CredentialId = credentialId;
        }

        public int ProgramId { get; set; }
        public int CredentialId { get; set; }
    }
}
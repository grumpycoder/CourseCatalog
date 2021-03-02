using MediatR;

namespace CourseCatalog.App.Features.Credentials.Commands.DeleteCredentialProgram
{
    public class DeleteCredentialProgramCommand : IRequest
    {
        public int ProgramId { get; set; }
        public int CredentialId { get; set; }

        public DeleteCredentialProgramCommand(int programId, int credentialId)
        {
            ProgramId = programId;
            CredentialId = credentialId;
        }
    }

}
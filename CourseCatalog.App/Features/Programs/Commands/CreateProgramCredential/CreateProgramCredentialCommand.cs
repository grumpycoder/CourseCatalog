using MediatR;

namespace CourseCatalog.App.Features.Programs.Commands.CreateProgramCredential
{
    public class CreateProgramCredentialCommand : IRequest<CreateProgramCredentialDto>
    {
        public int ProgramId { get; set; }
        public int CredentialId { get; set; }
        public int? BeginYear { get; set; }
        public int? EndYear { get; set; }
    }

    public class CreateProgramCredentialDto
    {
        public int ProgramCredentialId { get; set; }
        public int CredentialId { get; set; }
        public string CredentialCode { get; set; }
        public string CredentialName { get; set; }
        public int? BeginYear { get; set; }
        public int? EndYear { get; set; }
    }
}

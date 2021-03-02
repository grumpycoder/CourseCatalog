using MediatR;

namespace CourseCatalog.App.Features.Credentials.Commands.CreateCredentialProgram
{
    public class CreateCredentialProgramCommand : IRequest<CreateCredentialProgramDto>
    {
        public int ProgramId { get; set; }
        public int CredentialId { get; set; }
        public int? BeginYear { get; set; }
        public int? EndYear { get; set; }
    }

    public class CreateCredentialProgramDto
    {
        public int ProgramCredentialId { get; set; }
        public int ProgramId { get; set; }
        public string ProgramCode { get; set; }
        public string ProgramName { get; set; }
        public int? BeginYear { get; set; }
        public int? EndYear { get; set; }
    }
}

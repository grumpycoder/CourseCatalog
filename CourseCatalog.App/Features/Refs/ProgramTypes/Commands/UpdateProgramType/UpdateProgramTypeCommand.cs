using MediatR;

namespace CourseCatalog.App.Features.Refs.ProgramTypes.Commands.UpdateProgramType
{
    public class UpdateProgramTypeCommand : IRequest
    {
        public int ProgramTypeId { get; set; }
        public string Name { get; set; }
        public string ProgramTypeCode { get; set; }
        public string Description { get; set; }
    }
}
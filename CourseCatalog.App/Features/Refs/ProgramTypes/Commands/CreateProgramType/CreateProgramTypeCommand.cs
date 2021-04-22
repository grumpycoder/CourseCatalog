using MediatR;

namespace CourseCatalog.App.Features.Refs.ProgramTypes.Commands.CreateProgramType
{
    public class CreateProgramTypeCommand : IRequest<int>
    {
        public int ProgramTypeId { get; set; }
        public string Name { get; set; }
        public string ProgramTypeCode { get; set; }
        public string Description { get; set; }
    }
}
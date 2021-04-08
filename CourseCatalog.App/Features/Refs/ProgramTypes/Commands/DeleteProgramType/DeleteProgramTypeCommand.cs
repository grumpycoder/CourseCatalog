using MediatR;

namespace CourseCatalog.App.Features.Refs.ProgramTypes.Commands.DeleteProgramType
{
    public class DeleteProgramTypeCommand : IRequest
    {
        public int ProgramTypeId { get; set; }

        public DeleteProgramTypeCommand(int clusterTypeId)
        {
            ProgramTypeId = clusterTypeId;
        }
    }
}

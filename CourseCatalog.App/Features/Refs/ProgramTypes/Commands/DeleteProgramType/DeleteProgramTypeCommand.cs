using MediatR;

namespace CourseCatalog.App.Features.Refs.ProgramTypes.Commands.DeleteProgramType
{
    public class DeleteProgramTypeCommand : IRequest
    {
        public DeleteProgramTypeCommand(int clusterTypeId)
        {
            ProgramTypeId = clusterTypeId;
        }

        public int ProgramTypeId { get; set; }
    }
}
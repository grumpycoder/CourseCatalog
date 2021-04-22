using MediatR;

namespace CourseCatalog.App.Features.Programs.Queries.GetProgramDetail
{
    public class GetProgramDetailQuery : IRequest<ProgramDetailDto>
    {
        public GetProgramDetailQuery(int programId)
        {
            ProgramId = programId;
        }

        public int ProgramId { get; set; }
    }
}
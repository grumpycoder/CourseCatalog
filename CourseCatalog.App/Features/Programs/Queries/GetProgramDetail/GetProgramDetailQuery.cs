using MediatR;

namespace CourseCatalog.App.Features.Programs.Queries.GetProgramDetail
{
    public class GetProgramDetailQuery : IRequest<ProgramDetailDto>
    {
        public int ProgramId { get; set; }

        public GetProgramDetailQuery(int programId)
        {
            ProgramId = programId;
        }
    }
}

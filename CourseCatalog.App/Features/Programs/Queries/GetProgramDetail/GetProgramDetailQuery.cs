using MediatR;

namespace MyDemo.Api.Features.Programs.Queries.GetProgramDetail
{
    public class GetProgramDetailQuery : IRequest<ProgramDetailVm>
    {
        public int ProgramId { get; set; }
    }
}

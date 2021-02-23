using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Lookups.Queries.GetProgramList
{
    public class GetProgramListQueryHandler : IRequestHandler<GetProgramListQuery, List<Program>>
    {
        private readonly IAsyncRepository<Program> _repository;

        public GetProgramListQueryHandler(IAsyncRepository<Program> repository)
        {
            _repository = repository;
        }

        public async Task<List<Program>> Handle(GetProgramListQuery request, CancellationToken cancellationToken)
        {
            var dto = await _repository.ListAllAsync();
            return dto as List<Program>;
        }
    }
}

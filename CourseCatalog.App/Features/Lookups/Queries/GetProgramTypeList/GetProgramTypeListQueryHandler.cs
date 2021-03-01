using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Lookups.Queries.GetProgramTypeList
{
    public class GetProgramTypeListQueryHandler : IRequestHandler<GetProgramTypeListQuery, List<ProgramType>>
    {
        private readonly IAsyncRepository<ProgramType> _repository;

        public GetProgramTypeListQueryHandler(IAsyncRepository<ProgramType> repository)
        {
            _repository = repository;
        }

        public async Task<List<ProgramType>> Handle(GetProgramTypeListQuery request, CancellationToken cancellationToken)
        {
            var dto = await _repository.ListAllAsync();
            return dto as List<ProgramType>;
        }
    }
}

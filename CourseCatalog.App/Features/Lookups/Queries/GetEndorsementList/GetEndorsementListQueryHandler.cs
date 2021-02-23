using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Lookups.Queries.GetEndorsementList
{
    public class GetEndorsementListQueryHandler : IRequestHandler<GetEndorsementListQuery, List<Endorsement>>
    {
        private readonly IAsyncRepository<Endorsement> _repository;

        public GetEndorsementListQueryHandler(IAsyncRepository<Endorsement> repository)
        {
            _repository = repository;
        }

        public async Task<List<Endorsement>> Handle(GetEndorsementListQuery request, CancellationToken cancellationToken)
        {
            var dto = await _repository.ListAllAsync();
            return dto as List<Endorsement>;
        }
    }
}

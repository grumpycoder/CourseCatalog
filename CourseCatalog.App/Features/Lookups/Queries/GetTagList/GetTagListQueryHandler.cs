using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Lookups.Queries.GetTagList
{
    public class GetTagListQueryHandler : IRequestHandler<GetTagListQuery, List<Tag>>
    {
        private readonly ITagRepository _repository;

        public GetTagListQueryHandler(ITagRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Tag>> Handle(GetTagListQuery request, CancellationToken cancellationToken)
        {
            var dto = await _repository.ListAllAsync();
            return dto as List<Tag>;
        }
    }
}

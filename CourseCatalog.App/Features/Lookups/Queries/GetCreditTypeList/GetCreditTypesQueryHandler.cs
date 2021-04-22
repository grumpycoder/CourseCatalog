using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Lookups.Queries.GetCreditTypeList
{
    public class GetCreditTypeListQueryHandler : IRequestHandler<GetCreditTypeListQuery, List<Tag>>
    {
        private readonly ITagRepository _repository;

        public GetCreditTypeListQueryHandler(ITagRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Tag>> Handle(GetCreditTypeListQuery request, CancellationToken cancellationToken)
        {
            var dto = await _repository.GetCreditTypeTags();
            return dto;
        }
    }
}
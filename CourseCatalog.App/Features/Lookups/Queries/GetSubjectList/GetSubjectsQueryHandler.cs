using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Lookups.Queries.GetSubjectList
{
    public class GetSubjectListQueryHandler : IRequestHandler<GetSubjectListQuery, List<Subject>>
    {
        private readonly IAsyncRepository<Subject> _repository;

        public GetSubjectListQueryHandler(IAsyncRepository<Subject> repository)
        {
            _repository = repository;
        }

        public async Task<List<Subject>> Handle(GetSubjectListQuery request, CancellationToken cancellationToken)
        {
            var dto = await _repository.ListAllAsync();
            return dto as List<Subject>;
        }
    }
}
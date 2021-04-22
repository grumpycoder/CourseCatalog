using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Lookups.Queries.GetCipCodeList
{
    public class GetCipCodeListQueryHandler : IRequestHandler<GetCipCodeListQuery, List<Cip>>
    {
        private readonly IAsyncRepository<Cip> _repository;

        public GetCipCodeListQueryHandler(IAsyncRepository<Cip> repository)
        {
            _repository = repository;
        }

        public async Task<List<Cip>> Handle(GetCipCodeListQuery request, CancellationToken cancellationToken)
        {
            var dto = await _repository.ListAllAsync();
            return dto as List<Cip>;
        }
    }
}
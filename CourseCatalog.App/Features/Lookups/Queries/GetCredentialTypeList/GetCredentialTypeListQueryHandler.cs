using CourseCatalog.App.Features.Lookups.Queries.GetSubjectList;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Lookups.Queries.GetCredentialTypeList
{
    public class GetCredentialTypeListQueryHandler : IRequestHandler<GetCredentialTypeListQuery, List<CredentialType>>
    {
        private readonly IAsyncRepository<CredentialType> _repository;

        public GetCredentialTypeListQueryHandler(IAsyncRepository<CredentialType> repository)
        {
            _repository = repository;
        }

        public async Task<List<CredentialType>> Handle(GetCredentialTypeListQuery request, CancellationToken cancellationToken)
        {
            var dto = await _repository.ListAllAsync();
            return dto as List<CredentialType>;
        }
    }
}

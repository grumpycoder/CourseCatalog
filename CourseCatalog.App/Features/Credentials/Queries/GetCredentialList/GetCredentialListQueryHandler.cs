using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourseCatalog.Application.Contracts;
using MediatR;

namespace CourseCatalog.App.Features.Credentials.Queries.GetCredentialList
{
    public class GetCredentialListQueryHandler : IRequestHandler<GetCredentialListQuery, List<CredentialListDto>>
    {
        private readonly ICredentialRepository _credentialRepository;
        private readonly IMapper _mapper;

        public GetCredentialListQueryHandler(IMapper mapper, ICredentialRepository credentialRepository)
        {
            _mapper = mapper;
            _credentialRepository = credentialRepository;
        }

        public async Task<List<CredentialListDto>> Handle(GetCredentialListQuery request,
            CancellationToken cancellationToken)
        {
            var credentials = await _credentialRepository.GetCredentialsWithDetails();
            return _mapper.Map<List<CredentialListDto>>(credentials);
        }
    }
}
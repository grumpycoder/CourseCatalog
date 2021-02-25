using AutoMapper;
using MediatR;
using MyDemo.Api.Application.Contracts;
using MyDemo.Api.Domain.Entities;
using MyDemo.Api.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace MyDemo.Api.Features.Credentials.Queries.GetCredentialDetail
{
    public class GetCredentialDetailQueryHandler : IRequestHandler<GetCredentialDetailQuery, CredentialDetailVm>
    {
        private readonly ICredentialRepository _credentialRepository;
        private readonly IMapper _mapper;

        public GetCredentialDetailQueryHandler(IMapper mapper, ICredentialRepository credentialRepository
            )
        {
            _mapper = mapper;
            _credentialRepository = credentialRepository;
        }

        public async Task<CredentialDetailVm> Handle(GetCredentialDetailQuery request, CancellationToken cancellationToken)
        {
            var credential = await _credentialRepository.GetCredentialWithDetails(request.CredentialId);

            if (credential == null)
            {
                throw new NotFoundException(nameof(Credential), request.CredentialId);
            }
            var credentialDetailDto = _mapper.Map<CredentialDetailVm>(credential);

            return credentialDetailDto;
        }
    }
}

using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Credentials.Queries.GetCredentialDetail
{
    public class GetCredentialDetailQueryHandler : IRequestHandler<GetCredentialDetailQuery, CredentialDetailDto>
    {
        private readonly ICredentialRepository _credentialRepository;
        private readonly IMapper _mapper;

        public GetCredentialDetailQueryHandler(IMapper mapper, ICredentialRepository credentialRepository
            )
        {
            _mapper = mapper;
            _credentialRepository = credentialRepository;
        }

        public async Task<CredentialDetailDto> Handle(GetCredentialDetailQuery request, CancellationToken cancellationToken)
        {
            var credential = await _credentialRepository.GetCredentialByIdWithDetails(request.CredentialId);

            if (credential == null)
            {
                throw new NotFoundException(nameof(Credential), request.CredentialId);
            }
            var credentialDetailDto = _mapper.Map<CredentialDetailDto>(credential);

            return credentialDetailDto;
        }
    }
}

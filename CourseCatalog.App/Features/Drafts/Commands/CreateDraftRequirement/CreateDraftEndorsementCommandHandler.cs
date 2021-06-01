using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Drafts.Commands.CreateDraftRequirement
{
    public class
        CreateDraftEndorsementCommandHandler : IRequestHandler<CreateDraftEndorsementCommand,
            CreatedDraftEndorsementDto>
    {
        private readonly IDraftRepository _draftRepository;
        private readonly IMapper _mapper;

        public CreateDraftEndorsementCommandHandler(IMapper mapper, IDraftRepository draftRepository)
        {
            _mapper = mapper;
            _draftRepository = draftRepository;
        }

        public async Task<CreatedDraftEndorsementDto> Handle(CreateDraftEndorsementCommand request,
            CancellationToken cancellationToken)
        {
            var existingDraft = await _draftRepository.GetDraftByIdWithDetails(request.DraftId);

            if (existingDraft == null) throw new NotFoundException(nameof(Draft), request.DraftId);

            var existingEndorsement =
                existingDraft.Endorsements.FirstOrDefault(e => e.EndorsementId == request.EndorsementId);
            if (existingEndorsement != null)
                throw new BadRequestException($"Draft already contains existing endorsement {existingEndorsement.Endorsement.EndorseCode}");

            existingDraft.Endorsements.Add(new DraftEndorsement {EndorsementId = request.EndorsementId});

            await _draftRepository.UpdateAsync(existingDraft);

            existingDraft = await _draftRepository.GetDraftByIdWithDetails(request.DraftId);

            var endorsement = existingDraft.Endorsements.FirstOrDefault(e => e.EndorsementId == request.EndorsementId);

            var dto = _mapper.Map<CreatedDraftEndorsementDto>(endorsement);

            return dto;
        }
    }
}
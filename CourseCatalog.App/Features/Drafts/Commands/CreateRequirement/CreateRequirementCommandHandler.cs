using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Drafts.Commands.CreateRequirement
{
    public class CreateRequirementCommandHandler : IRequestHandler<CreateRequirementCommand, CreatedDraftEndorsementDto>
    {
        private readonly IMapper _mapper;
        private readonly IDraftRepository _draftRepository;
        private readonly ICourseRepository _courseRepository;

        public CreateRequirementCommandHandler(IMapper mapper, IDraftRepository draftRepository, ICourseRepository courseRepository)
        {
            _mapper = mapper;
            _draftRepository = draftRepository;
            _courseRepository = courseRepository;
        }

        public async Task<CreatedDraftEndorsementDto> Handle(CreateRequirementCommand request, CancellationToken cancellationToken)
        {
            var existingDraft = await _draftRepository.GetDraftByIdWithDetails(request.DraftId);

            if (existingDraft == null) throw new NotFoundException(nameof(Draft), request.DraftId);

            var existingEndorsement = existingDraft.Endorsements.FirstOrDefault(e => e.EndorsementId == request.EndorsementId);
            if (existingEndorsement != null) throw new BadRequestException($"Draft already contains existing endorsement");

            existingDraft.Endorsements.Add(new DraftEndorsement() { EndorsementId = request.EndorsementId });

            await _draftRepository.UpdateAsync(existingDraft);

            //TODO: too many database calls
            existingDraft = await _draftRepository.GetDraftByIdWithDetails(request.DraftId);

            var endorsement = existingDraft.Endorsements.FirstOrDefault(e => e.EndorsementId == request.EndorsementId);

            var dto = _mapper.Map<CreatedDraftEndorsementDto>(endorsement);

            return dto;
        }
    }
}

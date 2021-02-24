using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Drafts.Commands.DeleteRequirement
{
    public class DeleteRequirementCommandHandler : IRequestHandler<DeleteRequirementCommand>
    {
        private readonly IMapper _mapper;
        private readonly IDraftRepository _draftRepository;
        private readonly ICourseRepository _courseRepository;

        public DeleteRequirementCommandHandler(IMapper mapper, IDraftRepository draftRepository, ICourseRepository courseRepository)
        {
            _mapper = mapper;
            _draftRepository = draftRepository;
            _courseRepository = courseRepository;
        }

        public async Task<Unit> Handle(DeleteRequirementCommand request, CancellationToken cancellationToken)
        {
            var existingDraft = await _draftRepository.GetDraftByIdWithDetails(request.DraftId);

            if (existingDraft == null) throw new NotFoundException(nameof(Draft), request.DraftId);

            var endorsementToDelete = existingDraft.Endorsements.FirstOrDefault(e => e.EndorsementId == request.EndorsementId);
            if (endorsementToDelete == null) throw new BadRequestException($"Draft does not contains endorsement");

            existingDraft.Endorsements.Remove(endorsementToDelete);

            await _draftRepository.UpdateAsync(existingDraft);

            return Unit.Value;
        }
    }
}

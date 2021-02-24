using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Drafts.Commands.DeleteDraftProgram
{
    public class DeleteDraftProgramCommandHandler : IRequestHandler<DeleteDraftProgramCommand>
    {
        private readonly IMapper _mapper;
        private readonly IDraftRepository _draftRepository;
        private readonly ICourseRepository _courseRepository;

        public DeleteDraftProgramCommandHandler(IMapper mapper, IDraftRepository draftRepository, ICourseRepository courseRepository)
        {
            _mapper = mapper;
            _draftRepository = draftRepository;
            _courseRepository = courseRepository;
        }

        public async Task<Unit> Handle(DeleteDraftProgramCommand request, CancellationToken cancellationToken)
        {
            var existingDraft = await _draftRepository.GetDraftByIdWithDetails(request.DraftId);

            if (existingDraft == null) throw new NotFoundException(nameof(Draft), request.DraftId);

            var programToDelete = existingDraft.Programs.FirstOrDefault(e => e.ProgramId == request.ProgramId);
            if (programToDelete == null) throw new BadRequestException($"Draft does not contain program");

            existingDraft.Programs.Remove(programToDelete);

            await _draftRepository.UpdateAsync(existingDraft);

            return Unit.Value;
        }
    }
}

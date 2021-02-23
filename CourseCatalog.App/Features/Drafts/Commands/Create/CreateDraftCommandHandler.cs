using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Drafts.Commands.Create
{
    public class CreateDraftCommandHandler : IRequestHandler<CreateDraftCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IDraftRepository _draftRepository;
        private readonly ICourseRepository _courseRepository;

        public CreateDraftCommandHandler(IMapper mapper, IDraftRepository draftRepository, ICourseRepository courseRepository)
        {
            _mapper = mapper;
            _draftRepository = draftRepository;
            _courseRepository = courseRepository;
        }

        public async Task<int> Handle(CreateDraftCommand request, CancellationToken cancellationToken)
        {
            var existingDraft = await _draftRepository.GetDraftByCourseNumber(request.CourseNumber);

            if (existingDraft != null)throw new BadRequestException(
                $"Duplicate Draft Course Number. Existing draft already contains course number {request.CourseNumber}");

            var existingCourse = await _courseRepository.GetCourseByCourseNumber(request.CourseNumber); 
            if (existingCourse != null)throw new BadRequestException(
                $"Duplicate Course Number. Existing course already contains course number {request.CourseNumber}");

            var draft = _mapper.Map<Draft>(request);

            draft = await _draftRepository.AddAsync(draft);

            return draft.DraftId;
        }
    }
}

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Drafts.Commands.CreateDraftByCourseId
{
    public class CreateDraftByCourseIdCommandHandler : IRequestHandler<CreateDraftByCourseIdCommand, int>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IDraftRepository _draftRepository;
        private readonly IMapper _mapper;

        public CreateDraftByCourseIdCommandHandler(IMapper mapper, IDraftRepository draftRepository,
            ICourseRepository courseRepository)
        {
            _mapper = mapper;
            _draftRepository = draftRepository;
            _courseRepository = courseRepository;
        }

        public async Task<int> Handle(CreateDraftByCourseIdCommand request, CancellationToken cancellationToken)
        {
            var courseToDraft = await _courseRepository.GetCourseByIdWithDetails(request.CourseId);

            courseToDraft.Status = CourseStatus.InDraft;

            var existingDraft = await _draftRepository.GetDraftByCourseNumber(courseToDraft.CourseNumber);

            if (existingDraft != null)
                throw new BadRequestException(
                    $"Existing draft already contains course number {courseToDraft.CourseNumber}");

            var draftToCreate = new Draft();

            _mapper.Map(courseToDraft, draftToCreate, typeof(Course), typeof(Draft));

            courseToDraft.Endorsements
                .ForEach(endorsement =>
                {
                    draftToCreate.Endorsements.Add(new DraftEndorsement
                        {EndorsementId = endorsement.EndorsementId});
                });

            courseToDraft.Programs
                .ForEach(program =>
                {
                    draftToCreate.Programs.Add(new ProgramDraft
                        {ProgramId = program.ProgramId, BeginYear = program.BeginYear, EndYear = program.EndYear});
                });

            courseToDraft.DeliveryTypes
                .ForEach(program =>
                {
                    draftToCreate.DeliveryTypes.Add(new DraftDeliveryType
                        {DeliveryTypeId = program.DeliveryTypeId});
                });

            draftToCreate.Status = CourseStatus.ExistingCourse;

            await _draftRepository.AddAsync(draftToCreate);
            await _courseRepository.UpdateAsync(courseToDraft);

            return draftToCreate.DraftId;
        }
    }
}
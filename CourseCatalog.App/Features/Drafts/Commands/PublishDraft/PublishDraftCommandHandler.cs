using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebGrease.Css.Extensions;

namespace CourseCatalog.App.Features.Drafts.Commands.PublishDraft
{
    public class PublishDraftCommandHandler : IRequestHandler<PublishDraftCommand>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IDraftRepository _draftRepository;
        private readonly IMapper _mapper;
        private readonly IPublishCourseService _publishCourseService;

        public PublishDraftCommandHandler(IMapper mapper, IPublishCourseService publishCourseService,
            IDraftRepository draftRepository, ICourseRepository courseRepository)
        {
            _mapper = mapper;
            _publishCourseService = publishCourseService;
            _draftRepository = draftRepository;
            _courseRepository = courseRepository;
        }

        public async Task<Unit> Handle(PublishDraftCommand request, CancellationToken cancellationToken)
        {
            var draftToPublish = await _draftRepository.GetDraftByIdWithDetails(request.DraftId);
            draftToPublish.CourseNumber = draftToPublish.CourseNumber?.Replace("#", "");

            //Basic Validations
            if (draftToPublish.CourseNumber?.Length != 10)
                throw new BadRequestException("Cannot Publish. Invalid Course Number length");
            if (string.IsNullOrWhiteSpace(draftToPublish.Name))
                throw new BadRequestException("Cannot Publish. Invalid Course Name");

            if (draftToPublish.Status == CourseStatus.NewCourse)
            {
                var checkCourseExists = await _courseRepository.GetCourseByCourseNumber(draftToPublish.CourseNumber);
                if (checkCourseExists != null)
                    throw new BadRequestException(
                        $"Duplicate Course Number. Existing course already contains course number {draftToPublish.CourseNumber}");

                //create new course
                var courseToCreate = _mapper.Map<Course>(draftToPublish);

                courseToCreate.Status = CourseStatus.Published;
                courseToCreate.PublishDate = DateTime.Now;
                await _publishCourseService.PublishCourse(courseToCreate);

                //HACK: Is there another way to null list<string>?
                if (courseToCreate.Tags.Count == 0) courseToCreate.Tags = null;
                if (courseToCreate.CreditTypes.Count == 0) courseToCreate.CreditTypes = null;

                try
                {
                    var id = await _courseRepository.AddAsync(courseToCreate);
                    await _draftRepository.DeleteAsync(draftToPublish);
                }
                catch (Exception e)
                {
                    throw new Exception("Error moving draft to courses", e);
                }
            }
            else
            {
                var existingCourseCheck = await _courseRepository.GetCourseByCourseNumber(draftToPublish.CourseNumber);
                var existingCourse = await _courseRepository.GetCourseByIdWithDetails(existingCourseCheck.CourseId);

                _mapper.Map(draftToPublish, existingCourse, typeof(Draft), typeof(Course));

                //sync endorsements
                draftToPublish.Endorsements
                    .Where(draftEndorsement => existingCourse.Endorsements.All(courseEndorsement =>
                        courseEndorsement.EndorsementId != draftEndorsement.EndorsementId))
                    .ForEach(draftEndorsement =>
                    {
                        existingCourse.Endorsements.Add(new CourseEndorsement
                        { EndorsementId = draftEndorsement.EndorsementId });
                    });

                existingCourse.Endorsements.RemoveAll(courseEndorsement =>
                    !draftToPublish.Endorsements.Select(draftEndorsement => draftEndorsement.EndorsementId)
                        .Contains(courseEndorsement.EndorsementId));

                //sync programs
                draftToPublish.Programs
                    .Where(programDraft =>
                        existingCourse.Programs.All(programCourse => programCourse.ProgramId != programDraft.ProgramId))
                    .ForEach(programDraft =>
                    {
                        existingCourse.Programs.Add(new ProgramCourse
                        {
                            ProgramId = programDraft.ProgramId,
                            BeginYear = programDraft.BeginYear,
                            EndYear = programDraft.EndYear
                        });
                    });

                existingCourse.Programs.RemoveAll(programCourse =>
                    !draftToPublish.Programs.Select(programDraft => programDraft.ProgramId)
                        .Contains(programCourse.ProgramId));

                existingCourse.Programs.ForEach(programCourse =>
                {
                    var programDraft =
                        draftToPublish.Programs.FirstOrDefault(draft => draft.ProgramId == programCourse.ProgramId);
                    programCourse.BeginYear = programDraft?.BeginYear;
                    programCourse.EndYear = programDraft?.EndYear;
                });


                //sync delivery types
                draftToPublish.DeliveryTypes
                    .Where(draftDeliveryType => existingCourse.DeliveryTypes.All(courseDeliveryType =>
                        courseDeliveryType.DeliveryTypeId != draftDeliveryType.DeliveryTypeId))
                    .ForEach(draftDeliveryType =>
                    {
                        existingCourse.DeliveryTypes.Add(new CourseDeliveryType
                        { DeliveryTypeId = draftDeliveryType.DeliveryTypeId });
                    });

                existingCourse.DeliveryTypes.RemoveAll(courseDeliveryType =>
                    !draftToPublish.DeliveryTypes.Select(draftDeliveryType => draftDeliveryType.DeliveryTypeId)
                        .Contains(courseDeliveryType.DeliveryTypeId));


                existingCourse.Status = CourseStatus.Published;
                existingCourse.PublishDate = DateTime.Now;

                if (_publishCourseService == null) throw new Exception("No Publish service");
                await _publishCourseService.PublishCourse(existingCourse);

                //HACK: Is there another way to null list<string>?
                if (existingCourse.Tags.Count == 0) existingCourse.Tags = null;
                if (existingCourse.CreditTypes.Count == 0) existingCourse.CreditTypes = null;

                try
                {
                    await _courseRepository.UpdateAsync(existingCourse);
                    await _draftRepository.DeleteAsync(draftToPublish);
                }
                catch (Exception e)
                {
                    throw new Exception("Error moving draft to courses", e);
                }
            }

            return Unit.Value;
        }
    }
}
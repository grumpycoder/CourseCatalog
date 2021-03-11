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
        private readonly IMapper _mapper;
        private readonly IPublishCourseService _publishCourseService;
        private readonly IDraftRepository _draftRepository;
        private readonly ICourseRepository _courseRepository;

        public PublishDraftCommandHandler(IMapper mapper, IPublishCourseService publishCourseService, IDraftRepository draftRepository, ICourseRepository courseRepository)
        {
            _mapper = mapper;
            _publishCourseService = publishCourseService;
            _draftRepository = draftRepository;
            _courseRepository = courseRepository;
        }

        public async Task<Unit> Handle(PublishDraftCommand request, CancellationToken cancellationToken)
        {
            var draftToPublish = await _draftRepository.GetDraftByIdWithDetails(request.DraftId);
            draftToPublish.CourseNumber = draftToPublish.CourseNumber.Replace("#", "");

            //Basic Validations
            if (draftToPublish.CourseNumber.Length != 10) throw new BadRequestException("Cannot Publish. Invalid Course Number length");
            if (string.IsNullOrWhiteSpace(draftToPublish.Name)) throw new BadRequestException("Cannot Publish. Invalid Course Name");


            var existingCourse = await _courseRepository.GetCourseByCourseNumber(draftToPublish.CourseNumber);

            if (draftToPublish.Status == CourseStatus.NewCourse)
            {
                if (existingCourse != null)
                    throw new BadRequestException(
                        $"Duplicate Course Number. Existing course already contains course number {draftToPublish.CourseNumber}");

                //create new course
                existingCourse = _mapper.Map<Course>(draftToPublish);

                draftToPublish.Endorsements
                    .ForEach(endorsement => { existingCourse.Endorsements.Add(new CourseEndorsement { EndorsementId = endorsement.EndorsementId }); });

                draftToPublish.Programs
                    .ForEach(program => { existingCourse.Programs.Add(new ProgramCourse { ProgramId = program.ProgramId, BeginYear = program.BeginYear, EndYear = program.EndYear }); });

                draftToPublish.DeliveryTypes
                    .ForEach(program => { existingCourse.DeliveryTypes.Add(new CourseDeliveryType { DeliveryTypeId = program.DeliveryTypeId }); });


                existingCourse.Status = CourseStatus.Published;
                existingCourse.PublishDate = DateTime.Now;
                //var publishResponse = await _publishCourseService.PublishCourse(existingCourse);
                //if (!publishResponse.Success)
                //{
                //    throw new BadRequestException(publishResponse.Message);
                //}

                //TODO: Is there another way to null list<string>?
                if (existingCourse.Tags.Count == 0) existingCourse.Tags = null;
                if (existingCourse.CreditTypes.Count == 0) existingCourse.CreditTypes = null;

                var id = await _courseRepository.AddAsync(existingCourse);
                //await _draftRepository.DeleteAsync(draftToPublish);
            }
            else
            {
                existingCourse = await _courseRepository.GetCourseByIdWithDetails(existingCourse.CourseId);
                _mapper.Map(draftToPublish, existingCourse, typeof(Draft), typeof(Course));

                //sync endorsements
                draftToPublish.Endorsements
                    .Where(draftEndorsement => existingCourse.Endorsements.All(courseEndorsement => courseEndorsement.EndorsementId != draftEndorsement.EndorsementId))
                    .ForEach(draftEndorsement => { existingCourse.Endorsements.Add(new CourseEndorsement { EndorsementId = draftEndorsement.EndorsementId }); });

                existingCourse.Endorsements.RemoveAll(courseEndorsement => !draftToPublish.Endorsements.Select(draftEndorsement => draftEndorsement.EndorsementId).Contains(courseEndorsement.EndorsementId));

                //sync programs
                draftToPublish.Programs
                    .Where(programDraft => existingCourse.Programs.All(programCourse => programCourse.ProgramId != programDraft.ProgramId))
                    .ForEach(programDraft => { existingCourse.Programs.Add(new ProgramCourse { ProgramId = programDraft.ProgramId, BeginYear = programDraft.BeginYear, EndYear = programDraft.EndYear }); });

                existingCourse.Programs.RemoveAll(programCourse => !draftToPublish.Programs.Select(programDraft => programDraft.ProgramId).Contains(programCourse.ProgramId));

                existingCourse.Programs.ForEach(programCourse =>
                {
                    var programDraft = draftToPublish.Programs.FirstOrDefault(draft => draft.ProgramId == programCourse.ProgramId);
                    programCourse.BeginYear = programDraft?.BeginYear;
                    programCourse.EndYear = programDraft?.EndYear;
                });


                //sync delivery types
                draftToPublish.DeliveryTypes
                    .Where(draftDeliveryType => existingCourse.DeliveryTypes.All(courseDeliveryType => courseDeliveryType.DeliveryTypeId != draftDeliveryType.DeliveryTypeId))
                    .ForEach(draftDeliveryType => { existingCourse.DeliveryTypes.Add(new CourseDeliveryType { DeliveryTypeId = draftDeliveryType.DeliveryTypeId }); });

                existingCourse.DeliveryTypes.RemoveAll(courseDeliveryType => !draftToPublish.DeliveryTypes.Select(draftDeliveryType => draftDeliveryType.DeliveryTypeId).Contains(courseDeliveryType.DeliveryTypeId));


                existingCourse.Status = CourseStatus.Published;
                existingCourse.PublishDate = DateTime.Now;

                //var publishResponse = await _publishCourseService.PublishCourse(existingCourse);
                //if (!publishResponse.Success)
                //{
                //    throw new BadRequestException(publishResponse.Message);
                //}

                //TODO: Is there another way to null list<string>?
                if (existingCourse.Tags.Count == 0) existingCourse.Tags = null;
                if (existingCourse.CreditTypes.Count == 0) existingCourse.CreditTypes = null;

                await _courseRepository.UpdateAsync(existingCourse);

                //await _draftRepository.DeleteAsync(draftToPublish);
            }

            return Unit.Value;
        }
    }
}

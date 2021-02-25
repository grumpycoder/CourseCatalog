﻿using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CourseCatalog.Application.Exceptions;
using WebGrease.Css.Extensions;

namespace CourseCatalog.App.Features.Drafts.Commands.PublishDraft
{
    public class PublishDraftCommandHandler : IRequestHandler<PublishDraftCommand>
    {
        private readonly IMapper _mapper;
        private readonly IDraftRepository _draftRepository;
        private readonly ICourseRepository _courseRepository;

        public PublishDraftCommandHandler(IMapper mapper, IDraftRepository draftRepository, ICourseRepository courseRepository)
        {
            _mapper = mapper;
            _draftRepository = draftRepository;
            _courseRepository = courseRepository;
        }

        public async Task<Unit> Handle(PublishDraftCommand request, CancellationToken cancellationToken)
        {
            var draftToPublish = await _draftRepository.GetDraftByIdWithDetails(request.DraftId);
            var existingCourse = await _courseRepository.GetCourseByCourseNumber(draftToPublish.CourseNumber);

            if (draftToPublish.Status == CourseStatus.NewCourse)
            {
                if (existingCourse != null)
                    throw new BadRequestException(
                        $"Duplicate Course Number. Existing course already contains course number {draftToPublish.CourseNumber}");

                //create new course
                existingCourse = _mapper.Map<Course>(draftToPublish);
                existingCourse.Endorsements = new List<CourseEndorsement>();
                existingCourse.Programs = new List<ProgramCourse>();
                existingCourse.DeliveryTypes = new List<CourseDeliveryType>();

                draftToPublish.Endorsements
                    .ForEach(endorsement => { existingCourse.Endorsements.Add(new CourseEndorsement() { EndorsementId = endorsement.EndorsementId }); });

                draftToPublish.Programs
                    .ForEach(program => { existingCourse.Programs.Add(new ProgramCourse() { ProgramId = program.ProgramId, BeginYear = program.BeginYear, EndYear = program.EndYear }); });

                draftToPublish.DeliveryTypes
                    .ForEach(program => { existingCourse.DeliveryTypes.Add(new CourseDeliveryType() { DeliveryTypeId = program.DeliveryTypeId }); });

                existingCourse.Status = CourseStatus.Published;
                existingCourse.PublishDate = DateTime.Now;
                var id = await _courseRepository.AddAsync(existingCourse);

                await _draftRepository.DeleteAsync(draftToPublish);

            }
            else
            {
                existingCourse = await _courseRepository.GetCourseByIdWithDetails(existingCourse.CourseId);
                _mapper.Map(draftToPublish, existingCourse, typeof(Draft), typeof(Course));

                //sync endorsements
                draftToPublish.Endorsements
                    .Where(draftEndorsement => existingCourse.Endorsements.All(courseEndorsement => courseEndorsement.EndorsementId != draftEndorsement.EndorsementId))
                    .ForEach(draftEndorsement => { existingCourse.Endorsements.Add(new CourseEndorsement() { EndorsementId = draftEndorsement.EndorsementId }); });

                existingCourse.Endorsements.RemoveAll(courseEndorsement => !draftToPublish.Endorsements.Select(draftEndorsement => draftEndorsement.EndorsementId).Contains(courseEndorsement.EndorsementId));

                //sync programs
                draftToPublish.Programs
                    .Where(programDraft => existingCourse.Programs.All(programCourse => programCourse.ProgramId != programDraft.ProgramId))
                    .ForEach(programDraft => { existingCourse.Programs.Add(new ProgramCourse() { ProgramId = programDraft.ProgramId, BeginYear = programDraft.BeginYear, EndYear = programDraft.EndYear }); });

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
                    .ForEach(draftDeliveryType => { existingCourse.DeliveryTypes.Add(new CourseDeliveryType() { DeliveryTypeId = draftDeliveryType.DeliveryTypeId }); });

                existingCourse.DeliveryTypes.RemoveAll(courseDeliveryType => !draftToPublish.DeliveryTypes.Select(draftDeliveryType => draftDeliveryType.DeliveryTypeId).Contains(courseDeliveryType.DeliveryTypeId));

                existingCourse.Status = CourseStatus.Published;
                existingCourse.PublishDate = DateTime.Now;

                await _courseRepository.UpdateAsync(existingCourse);

                await _draftRepository.DeleteAsync(draftToPublish);
            }

            //TODO: publish course to PS

            return Unit.Value;
        }
    }
}

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Refs.CourseLevels.Commands.UpdateCourseLevel
{
    public class UpdateCourseLevelCommandHandler : IRequestHandler<UpdateCourseLevelCommand>
    {
        private readonly ICourseLevelRepository _courseLevelRepository;
        private readonly IMapper _mapper;

        public UpdateCourseLevelCommandHandler(IMapper mapper, ICourseLevelRepository courseLevelRepository)
        {
            _mapper = mapper;
            _courseLevelRepository = courseLevelRepository;
        }

        public async Task<Unit> Handle(UpdateCourseLevelCommand request, CancellationToken cancellationToken)
        {
            var courseLevelToUpdate = await _courseLevelRepository.GetByIdAsync(request.CourseLevelId);
            if (courseLevelToUpdate == null) throw new NotFoundException(nameof(Subject), request.CourseLevelId);

            var subject = await _courseLevelRepository.GetCourseLevelByCourseLevelCode(request.CourseLevelCode);
            if (subject != null && subject.CourseLevelCode != courseLevelToUpdate.CourseLevelCode)
                throw new BadRequestException(
                    $"Duplicate CourseLevel Code. Existing Course Level already contains Course Level Code {request.CourseLevelCode}");

            _mapper.Map(request, courseLevelToUpdate, typeof(UpdateCourseLevelCommand), typeof(CourseLevel));

            await _courseLevelRepository.UpdateAsync(courseLevelToUpdate);

            return Unit.Value;
        }
    }
}
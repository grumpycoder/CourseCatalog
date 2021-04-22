using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Refs.CourseLevels.Commands.CreateCourseLevel
{
    public class CreateCourseLevelCommandHandler : IRequestHandler<CreateCourseLevelCommand, int>
    {
        private readonly ICourseLevelRepository _courseLevelRepository;
        private readonly IMapper _mapper;

        public CreateCourseLevelCommandHandler(IMapper mapper, ICourseLevelRepository courseLevelRepository)
        {
            _mapper = mapper;
            _courseLevelRepository = courseLevelRepository;
        }

        public async Task<int> Handle(CreateCourseLevelCommand request, CancellationToken cancellationToken)
        {
            var courseLevel = await _courseLevelRepository.GetCourseLevelByCourseLevelCode(request.CourseLevelCode);
            if (courseLevel != null)
                throw new BadRequestException(
                    $"Duplicate Course Level Code. Existing Course Level already contains Course Level Code {request.CourseLevelCode}");

            courseLevel = _mapper.Map<CourseLevel>(request);

            courseLevel = await _courseLevelRepository.AddAsync(courseLevel);

            return courseLevel.CourseLevelId;
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Courses.Queries.GetCourseDetail
{
    public class GetCourseDetailQueryHandler : IRequestHandler<GetCourseDetailQuery, CourseDetailDto>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public GetCourseDetailQueryHandler(IMapper mapper, ICourseRepository courseRepository)
        {
            _mapper = mapper;
            _courseRepository = courseRepository;
        }

        public async Task<CourseDetailDto> Handle(GetCourseDetailQuery request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetCourseByIdWithDetails(request.CourseId);

            if (course == null) throw new NotFoundException(nameof(Course), request.CourseId);
            var courseDetailDto = _mapper.Map<CourseDetailDto>(course);

            return courseDetailDto;
        }
    }
}
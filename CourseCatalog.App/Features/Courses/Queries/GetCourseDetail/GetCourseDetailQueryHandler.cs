using System.Threading;
using System.Threading.Tasks;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Courses.Queries.GetCourseDetail
{
    public class GetCourseDetailQueryHandler : IRequestHandler<GetCourseDetailQuery, CourseDetailDto>
    {
        private readonly IAsyncRepository<Course> _courseRepository;
        //private readonly IMapper _mapper;

        //public GetCourseDetailQueryHandler(IMapper mapper, IAsyncRepository<Course> courseRepository
        public GetCourseDetailQueryHandler(IAsyncRepository<Course> courseRepository)
        {
            //_mapper = mapper;
            _courseRepository = courseRepository;
        }

        public async Task<CourseDetailDto> Handle(GetCourseDetailQuery request, CancellationToken cancellationToken)
        {
            //var course = await _courseRepository.GetCourseWithDetails(request.CourseId);
            var course = await _courseRepository.GetByIdAsync(request.CourseId);

            if (course == null)
            {
                throw new NotFoundException(nameof(Course), request.CourseId);
            }
            //var courseDetailDto = _mapper.Map<CourseDetailVm>(course);
            var courseDetailDto = new CourseDetailDto()
            {
                CourseNumber = course.CourseNumber
            };

            return courseDetailDto;
        }
    }
}

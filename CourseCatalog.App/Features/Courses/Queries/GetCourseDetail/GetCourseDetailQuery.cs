using MediatR;

namespace CourseCatalog.App.Features.Courses.Queries.GetCourseDetail
{
    public class GetCourseDetailQuery : IRequest<CourseDetailDto>
    {
        public int CourseId { get; set; }
    }
}

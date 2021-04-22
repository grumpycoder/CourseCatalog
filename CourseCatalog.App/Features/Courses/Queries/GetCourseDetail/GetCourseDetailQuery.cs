using MediatR;

namespace CourseCatalog.App.Features.Courses.Queries.GetCourseDetail
{
    public class GetCourseDetailQuery : IRequest<CourseDetailDto>
    {
        public GetCourseDetailQuery(int courseId)
        {
            CourseId = courseId;
        }

        public int CourseId { get; }
    }
}
using MediatR;

namespace CourseCatalog.App.Features.Courses.Queries.GetXmlCourseList
{
    public class GetXmlCourseListQuery : IRequest<string>
    {
        public int SchoolYear { get; private set; }

        public GetXmlCourseListQuery(int schoolYear)
        {
            SchoolYear = schoolYear;
        }
    }
}

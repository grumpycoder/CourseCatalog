using CourseCatalog.Application.Contracts;
using CourseCatalog.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Courses.Queries.GetXmlCourseList
{
    public class GetXmlCourseListQueryHandlder : IRequestHandler<GetXmlCourseListQuery, string>
    {
        private readonly ICourseRepository _courseRepository;

        public GetXmlCourseListQueryHandlder(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<string> Handle(GetXmlCourseListQuery request, CancellationToken cancellationToken)
        {
            var result = await _courseRepository.GetCourseXml(request.SchoolYear);
            return result;
        }
    }
}

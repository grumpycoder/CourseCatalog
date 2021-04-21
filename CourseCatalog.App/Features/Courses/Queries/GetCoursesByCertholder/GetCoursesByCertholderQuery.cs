using MediatR;
using System.Collections.Generic;

namespace CourseCatalog.App.Features.Courses.Queries.GetCoursesByCertholder
{
    public class GetCoursesByCertholderQuery : IRequest<List<CertholderCourseListDto>>
    {
        public int CertholderId { get; }

        public GetCoursesByCertholderQuery(int certholderId)
        {
            CertholderId = certholderId;
        }

    }
}

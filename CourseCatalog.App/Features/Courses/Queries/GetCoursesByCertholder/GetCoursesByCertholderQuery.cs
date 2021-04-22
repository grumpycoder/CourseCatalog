using System.Collections.Generic;
using MediatR;

namespace CourseCatalog.App.Features.Courses.Queries.GetCoursesByCertholder
{
    public class GetCoursesByCertholderQuery : IRequest<List<CertholderCourseListDto>>
    {
        public GetCoursesByCertholderQuery(int certholderId)
        {
            CertholderId = certholderId;
        }

        public int CertholderId { get; }
    }
}
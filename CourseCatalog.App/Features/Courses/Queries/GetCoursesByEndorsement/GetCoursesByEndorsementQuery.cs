using System.Collections.Generic;
using MediatR;

namespace CourseCatalog.App.Features.Courses.Queries.GetCoursesByEndorsement
{
    public class GetCoursesByEndorsementQuery : IRequest<List<EndorsementCourseListDto>>
    {
        public int EndorseId { get; }

        public GetCoursesByEndorsementQuery(int endorseId)
        {
            EndorseId = endorseId;
        }

    }
}

using CourseCatalog.App.Features.Courses.Queries.GetCourseDetail;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;

namespace CourseCatalog.App.Controllers.API
{
    [RoutePrefix("api/courses")]
    public class CoursesController : ApiController
    {
        private readonly IMediator _mediator;

        public CoursesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet, Route("{courseId}")]
        public async Task<IHttpActionResult> Get(int courseId)
        {
            var dto = await _mediator.Send(new GetCourseDetailQuery() { CourseId = courseId });
            return Ok(dto);
        }

    }
}

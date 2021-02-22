using CourseCatalog.App.Features.Courses.Queries.GetCourseDetail;
using CourseCatalog.Persistence;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using MediatR;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace CourseCatalog.App.Controllers.API
{
    [RoutePrefix("api/courses")]
    public class CoursesController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly CourseDbContext _context;

        public CoursesController(IMediator mediator, CourseDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        [HttpGet, Route("active")]
        public async Task<IHttpActionResult> GetActive(DataSourceLoadOptions loadOptions)
        {
            try
            {
                var dtos = await DataSourceLoader.LoadAsync(_context.CoursesView.Where(c => !c.IsRetired), loadOptions);

                return Ok(dtos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            try
            {
                var list = await DataSourceLoader.LoadAsync(_context.CoursesView, loadOptions);

                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("{courseId}")]
        public async Task<IHttpActionResult> Get(int courseId)
        {
            var dto = await _mediator.Send(new GetCourseDetailQuery(courseId));
            return Ok(dto);
        }

    }
}

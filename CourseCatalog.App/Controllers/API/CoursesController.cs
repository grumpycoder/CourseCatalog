using CourseCatalog.App.Features.Courses.Queries.GetCourseDetail;
using CourseCatalog.App.Features.Courses.Queries.GetCoursesByEndorsement;
using CourseCatalog.App.Features.Courses.Queries.GetCourseSummary;
using CourseCatalog.Persistence;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using MediatR;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet, Route("endorsements/{endorseId}")]
        public async Task<IHttpActionResult> GetCoursesByEndorsement(int endorseId)
        {
            var dtos = await _mediator.Send(new GetCoursesByEndorsementQuery(endorseId));
            return Ok(dtos);
        }

        [HttpGet, Route("{courseId}/teachers")]
        public async Task<IHttpActionResult> GetTeachersByCourse(int courseId, DataSourceLoadOptions loadOptions)
        {
            //HACK: Need to refactor b/c used devx load options
            var course = await _context.Courses
                    .Include(e => e.Endorsements)
                    .FirstOrDefaultAsync(c => c.CourseId == courseId);

            var endorsements = course.Endorsements.Select(n => n.EndorsementId).ToList();

            var dtos = await DataSourceLoader
                .LoadAsync(_context.Certificates.Where(c => endorsements
                    .Contains(c.EndorsementId)), loadOptions);

            return Ok(dtos);
        }

        [HttpGet, Route("summary")]
        public async Task<IHttpActionResult> Summary()
        {
            var dto = await _mediator.Send(new GetCourseSummaryQuery());
            return Ok(dto);
        }
    }
}

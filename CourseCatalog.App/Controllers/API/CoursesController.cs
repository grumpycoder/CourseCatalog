using CourseCatalog.App.Features.Courses.Queries.GetCourseDetail;
using CourseCatalog.App.Features.Courses.Queries.GetCoursesByEndorsement;
using CourseCatalog.App.Features.Courses.Queries.GetCourseSummary;
using CourseCatalog.App.Features.Courses.Queries.GetXmlCourseList;
using CourseCatalog.Persistence;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace CourseCatalog.App.Controllers.API
{
    [RoutePrefix("api/courses")]
    public class CoursesController : ApiController
    {
        private readonly CourseDbContext _context;
        private readonly IMediator _mediator;

        public CoursesController(IMediator mediator, CourseDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        [HttpGet]
        [Route("active")]
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

        [HttpGet]
        [Route("")]
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

        [HttpGet]
        [Route("{courseId}")]
        public async Task<IHttpActionResult> Get(int courseId)
        {
            var dto = await _mediator.Send(new GetCourseDetailQuery(courseId));
            return Ok(dto);
        }

        [HttpGet]
        [Route("endorsements/{endorseId}")]
        public async Task<IHttpActionResult> GetCoursesByEndorsement(int endorseId)
        {
            var dtos = await _mediator.Send(new GetCoursesByEndorsementQuery(endorseId));
            return Ok(dtos);
        }

        [HttpGet]
        [Route("{courseId}/teachers")]
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

        [HttpGet]
        [Route("summary")]
        public async Task<IHttpActionResult> Summary()
        {
            var dto = await _mediator.Send(new GetCourseSummaryQuery());
            return Ok(dto);
        }

        [HttpGet]
        [Route("xml/{schoolYear}")]
        public async Task<HttpResponseMessage> GetXmlByYear(int schoolYear)
        {

            var xml = await _mediator.Send(new GetXmlCourseListQuery(schoolYear));

            HttpResponseMessage result = new HttpResponseMessage(System.Net.HttpStatusCode.OK);

            result.Content = new StringContent(xml);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment"); //attachment will force download
            result.Content.Headers.ContentDisposition.FileName = $"StateCodesList-{schoolYear}.xml";

            return result;
        }

        [HttpGet]
        [Route("xml")]
        public async Task<HttpResponseMessage> GetXmlCurrentYear()
        {
            var schoolYear = DateTime.Now.Year;

            var xml = await _mediator.Send(new GetXmlCourseListQuery(schoolYear));

            HttpResponseMessage result = new HttpResponseMessage(System.Net.HttpStatusCode.OK);

            result.Content = new StringContent(xml);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment"); //attachment will force download
            result.Content.Headers.ContentDisposition.FileName = $"StateCodesList-{schoolYear}.xml";

            return result;
        }
    }

    public class Sti
    {
        public string Xml { get; set; }
    }
}
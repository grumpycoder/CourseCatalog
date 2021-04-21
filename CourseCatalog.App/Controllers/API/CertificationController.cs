using CourseCatalog.Persistence;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using CourseCatalog.App.Features.Courses.Queries.GetCoursesByCertholder;

namespace CourseCatalog.App.Controllers.API
{
    [RoutePrefix("api/certification")]
    public class CertificationController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly CourseDbContext _context;

        public CertificationController(IMediator mediator, CourseDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        [HttpGet, Route("certholders")]
        public async Task<IHttpActionResult> Certholders(DataSourceLoadOptions loadOptions)
        {
            //HACK: Need to refactor b/c used devx load options
            var dtos = await DataSourceLoader
                .LoadAsync(_context.Certholders, loadOptions);
            return Ok(dtos);
        }


        [HttpGet, Route("{certholderId}/courses")]
        public async Task<IHttpActionResult> GetCoursesByCertholder(int certholderId)
        {
            var dtos = await _mediator.Send(new GetCoursesByCertholderQuery(certholderId));
            return Ok(dtos);
        }

        //[HttpGet, Route("{certholderId}/courses")]
        //public async Task<IHttpActionResult> GetCertholderCourses(int certholderId, DataSourceLoadOptions loadOptions)
        //{
        //    //HACK: Need to refactor b/c used devx load options
        //    var course = await _context.Courses
        //        .Include(e => e.Endorsements)
        //        .FirstOrDefaultAsync(c => c.CourseId == certholderId);

        //    var endorsements = course.Endorsements.Select(n => n.EndorsementId);

        //    var dtos = await DataSourceLoader
        //        .LoadAsync(_context.Certificates.Where(c => endorsements.Contains(c.EndorsementId)), loadOptions);
        //    return Ok(dtos);
        //}
    }
}
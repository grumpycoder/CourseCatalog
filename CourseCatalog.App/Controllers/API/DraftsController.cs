using System;
using System.Linq;
using CourseCatalog.App.Features.Drafts.Queries.GetDraftDetail;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using CourseCatalog.Domain.Entities;
using CourseCatalog.Persistence;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;

namespace CourseCatalog.App.Controllers.API
{
    [RoutePrefix("api/drafts")]
    public class DraftsController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly CourseDbContext _context;

        public DraftsController(IMediator mediator, CourseDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            try
            {
                //var list = await DataSourceLoader.LoadAsync(_context.Drafts.Where(x => x.Status != CourseStatus.Published).ProjectTo<CourseDto>(), loadOptions);
                var list = await DataSourceLoader.LoadAsync(_context.Drafts.Where(x => x.Status != CourseStatus.Published), loadOptions);

                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("{draftId}")]
        public async Task<IHttpActionResult> Get(int draftId)
        {
            var dto = await _mediator.Send(new GetDraftDetailQuery(draftId));
            return Ok(dto);
        }

    }
}

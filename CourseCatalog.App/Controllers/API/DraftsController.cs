using CourseCatalog.App.Features.Drafts.Queries.GetDraftDetail;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;

namespace CourseCatalog.App.Controllers.API
{
    [RoutePrefix("api/drafts")]
    public class DraftsController : ApiController
    {
        private readonly IMediator _mediator;

        public DraftsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet, Route("{draftId}")]
        public async Task<IHttpActionResult> Get(int draftId)
        {
            var dto = await _mediator.Send(new GetDraftDetailQuery(draftId));
            return Ok(dto);
        }

    }
}

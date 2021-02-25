using CourseCatalog.App.Features.Clusters.Queries.GetClusterList;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;

namespace CourseCatalog.App.Controllers.API
{
    [RoutePrefix("api/clusters")]
    public class ClustersController : ApiController
    {
        private readonly IMediator _mediator;

        public ClustersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var dtos = await _mediator.Send(new GetClusterListQuery());
            return Ok(dtos);
        }
    }

}

using CourseCatalog.App.Features.Clusters.Commands.UpdateCluster;
using CourseCatalog.App.Features.Clusters.Queries.GetClusterDetail;
using CourseCatalog.App.Features.Clusters.Queries.GetClusterList;
using CourseCatalog.App.Filters;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using CourseCatalog.App.Features.Clusters.Commands.CreateCluster;

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

        [HttpGet, Route("{clusterId}")]
        public async Task<IHttpActionResult> Get(int clusterId)
        {
            var cluster = await _mediator.Send(new GetClusterDetailQuery(clusterId));
            return Ok(cluster);
        }

        [HttpPut, Route, CustomAuthorize(Roles = "CareerTechAdmin, Admin")]
        public async Task<IHttpActionResult> UpdateCluster([FromBody] UpdateClusterCommand updateClusterCommand)
        {
            var id = await _mediator.Send(updateClusterCommand);
            return Ok(id);
        }

        [HttpPost, Route, CustomAuthorize(Roles = "CareerTechAdmin, Admin")]
        public async Task<IHttpActionResult> CreateCluster([FromBody] CreateClusterCommand createClusterCommand)
        {
            var id = await _mediator.Send(createClusterCommand);
            return Ok(id);
        }

    }

}

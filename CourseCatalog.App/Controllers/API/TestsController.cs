using System.Threading.Tasks;
using System.Web.Http;

namespace CourseCatalog.App.Controllers.API
{
    [RoutePrefix("tests")]
    public class TestsController : ApiController
    {

        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok("Get Request");
        }

        [HttpPut, Route("")]
        public async Task<IHttpActionResult> Put()
        {
            return Ok("Put Request");
        }

        [HttpDelete, Route("")]
        public async Task<IHttpActionResult> Delete()
        {
            return Ok("Delete Request");
        }

    }
}

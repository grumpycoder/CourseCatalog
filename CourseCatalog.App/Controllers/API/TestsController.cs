using System;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using CourseCatalog.App.Services;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CourseCatalog.App.Controllers.API
{
    [RoutePrefix("api/tests")]
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

        [HttpPost, Route("throwError")]
        public async Task<IHttpActionResult> ThrowError()
        {
            throw new Exception("Testing error handling");
            return Ok();
        }

        [HttpPost, Route("PsRequest")]
        public async Task<IHttpActionResult> PsClientRequest()
        {
            var url = ConfigurationManager.AppSettings["ApiRequestUrl"];
            var config = new PublisherApiConfiguration();
            var result = await url
                .AppendPathSegment(Uri.EscapeUriString("oauth/access_token"))
                .SetQueryParam("grant_type", "client_credentials")
                .SetQueryParam("client_id", config.ApiPluginClientId)
                .SetQueryParam("client_secret", config.ClientSecret)
                .WithHeader("Content-Type", "application/x-www-form-urlencoded")
                .PostAsync().ReceiveJson();

            var access_token = result.access_token;
            var token_type = result.token_type;
            var expires_in = result.expires_in;

            string bearer = access_token.ToString(); 

            var courses = await "https://algold-pilot.powerschool.com"
                .AppendPathSegment("ws/schema/table/u_def_courses")
                .SetQueryParam("projection", "*")
                .WithOAuthBearerToken(bearer)
                //.WithOAuthBearerToken(access_token.ToString())
                .GetJsonAsync();

            return Ok(courses);

            //var url = "https://devcourses.alsde.edu/api/drafts"; //.AppendPathSegment("4");
            //var result = await url.AppendPathSegment("6").GetJsonAsync(); 

            return Ok(result);
        }
    }
}

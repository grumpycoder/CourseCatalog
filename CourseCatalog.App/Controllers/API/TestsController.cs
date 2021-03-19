using AutoMapper;
using CourseCatalog.App.Services;
using CourseCatalog.Application.Contracts;
using Flurl;
using Flurl.Http;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Http;

namespace CourseCatalog.App.Controllers.API
{
    [RoutePrefix("api/tests")]
    public class TestsController : ApiController
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public TestsController(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

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

        [HttpPost, Route("PsCourseRequest")]
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
        }

        [HttpPost, Route("PsCoursePost/{courseId}")]
        public async Task<IHttpActionResult> PsClientpost(int courseId)
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
            string bearer = access_token.ToString();

            var course = await _courseRepository.GetCourseByIdWithDetails(courseId);

            var dto = _mapper.Map<UDefCourses>(course);
            if (dto.CreditType == null) dto.CreditType = "";

            var container = new UDefCoursesContainer()
            {
                Name = "u_def_courses",
                Tables = new Tables() { UDefCourses = dto }
            };

            var post = await "https://algold-pilot.powerschool.com"
                .AppendPathSegment("ws/schema/table/u_def_courses")
                .WithOAuthBearerToken(bearer)
                .WithHeader("Content-Type", "application/json")
                .PostJsonAsync(container)
                .ReceiveJson();

            var status = post.result[0];

            return Ok(status.status);
        }
    }
}

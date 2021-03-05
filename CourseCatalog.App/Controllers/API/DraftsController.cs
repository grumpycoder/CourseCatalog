using AutoMapper;
using CourseCatalog.App.Features.Drafts.Commands.CreateDraft;
using CourseCatalog.App.Features.Drafts.Commands.CreateDraftByCourseId;
using CourseCatalog.App.Features.Drafts.Commands.CreateDraftProgram;
using CourseCatalog.App.Features.Drafts.Commands.CreateDraftRequirement;
using CourseCatalog.App.Features.Drafts.Commands.DeleteDraft;
using CourseCatalog.App.Features.Drafts.Commands.DeleteDraftProgram;
using CourseCatalog.App.Features.Drafts.Commands.DeleteRequirement;
using CourseCatalog.App.Features.Drafts.Commands.PublishDraft;
using CourseCatalog.App.Features.Drafts.Commands.UpdateDraft;
using CourseCatalog.App.Features.Drafts.Queries.GetDraftDetail;
using CourseCatalog.App.Services;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Persistence;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using CourseCatalog.Domain.Entities;

namespace CourseCatalog.App.Controllers.API
{
    [RoutePrefix("api/drafts")]
    public class DraftsController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly CourseDbContext _context;
        private readonly IPublishCourseService _publishCourseService;
        private readonly IMapper _mapper;

        public DraftsController(IMediator mediator, CourseDbContext context, IPublishCourseService publishCourseService, IMapper mapper)
        {
            _mediator = mediator;
            _context = context;
            _publishCourseService = publishCourseService;
            _mapper = mapper;
        }

        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            try
            {
                //var list = await DataSourceLoader.LoadAsync(_context.Drafts.Where(x => x.Status != CourseStatus.Published).ProjectTo<CourseDto>(), loadOptions);
                var list = await DataSourceLoader.LoadAsync(_context.DraftsView, loadOptions);

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

        [HttpPut, Route]
        public async Task<IHttpActionResult> Put([FromBody] UpdateDraftCommand updateDraftCommand)
        {
            var dto = await _mediator.Send(updateDraftCommand);
            return Ok(dto);
        }

        [HttpPost, Route]
        public async Task<IHttpActionResult> Post([FromBody] CreateDraftCommand createDraftCommand)
        {
            var dto = await _mediator.Send(createDraftCommand);
            return Ok(dto);
        }

        [HttpDelete, Route("{draftId}")]
        public async Task<IHttpActionResult> Delete(int draftId)
        {
            var dto = await _mediator.Send(new DeleteDraftCommand(draftId));
            return Ok(dto);
        }

        [HttpPost, Route("createendorsement")]
        public async Task<IHttpActionResult> CreateDraftEndorsement([FromBody] CreateDraftEndorsementCommand createRequirementCommand)
        {
            var dto = await _mediator.Send(createRequirementCommand);
            return Ok(dto);
        }

        [HttpDelete]
        [Route("{draftId}/endorsements/{endorsementId}")]
        public async Task<IHttpActionResult> DeleteDraftEndorsement(int draftId, int endorsementId)
        {
            await _mediator.Send(new DeleteRequirementCommand(draftId, endorsementId));
            return Ok();
        }

        [HttpPost, Route("assignprogram")]
        public async Task<IHttpActionResult> AssignProgram([FromBody] CreateDraftProgramCommand createDraftProgramCommand)
        {
            var dto = await _mediator.Send(createDraftProgramCommand);
            return Ok(dto);
        }

        [HttpDelete]
        //[Route("{draftId}/programs/{programId}"), Authorize(Roles = "CourseAdmin")]
        [Route("{draftId}/programs/{programId}")]
        public async Task<IHttpActionResult> RemoveProgram(int draftId, int programId)
        {
            await _mediator.Send(new DeleteDraftProgramCommand(draftId, programId));
            return Ok();
        }

        [HttpPost, Route("{courseId}/create")]
        public async Task<IHttpActionResult> CreateDraft(int courseId)
        {
            var dto = await _mediator.Send(new CreateDraftByCourseIdCommand(courseId));
            return Ok(dto);
        }

        [HttpPost, Route("publish/{draftId}")]
        public async Task<IHttpActionResult> Post(int draftId)
        {
            var dto = await _mediator.Send(new PublishDraftCommand(draftId));
            return Ok(dto);
        }

        [HttpGet, Route("test")]
        public async Task<IHttpActionResult> Test()
        {
            

            var course = await _context.Courses
                .Include(c => c.LowGrade)
                .Include(c => c.HighGrade)
                .Include(c => c.Subject)
                .Include(c => c.Endorsements).ThenInclude(e => e.Endorsement)
                .FirstAsync(c => c.CourseId == 5);

            var counter = await _publishCourseService.PublishCourse(course);
            return Ok(counter);

            var dto = _mapper.Map<UDefCourses>(course);
            var u = new UDefCoursesContainer()
            {
                Name = "u_def_courses",
                Tables = new Tables { UDefCourses = dto }
            };

            

            //var dto = await _mediator.Send(new PublishDraftCommand(draftId));
            var bearerToken = "58f50a4e-9a82-4e82-90a2-8154614d3d9a"; //await _publishCourseService.PublishCourse(course);
            var apiEndpoint = "https://applegrove-al7.powerschool.com/ws/schema/table/u_def_courses";
            var client = MethodHeaders(bearerToken, apiEndpoint);
            var json = JsonConvert.SerializeObject(u);
            var request =
                new HttpRequestMessage(HttpMethod.Post, Uri.EscapeUriString(client.BaseAddress.ToString()))
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };

            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var tokenResponse = await client.PostAsync(Uri.EscapeUriString(client.BaseAddress.ToString()), request.Content); //.Result;

            if (tokenResponse.IsSuccessStatusCode)
            {
                var response = await tokenResponse.Content.ReadAsStringAsync(); //<string>(new[] { new JsonMediaTypeFormatter() });
                return Ok(response);
            }

            return Ok(tokenResponse);

            //return Ok(u);
        }

        private static HttpClient MethodHeaders(string bearerToken, string endpointUrl)
        {
            var handler = new HttpClientHandler() { UseDefaultCredentials = false };
            var client = new HttpClient(handler);

            try
            {
                client.BaseAddress = new Uri(endpointUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + bearerToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return client;
        }
    }
}

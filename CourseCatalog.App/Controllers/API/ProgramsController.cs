using CourseCatalog.App.Features.Lookups.Queries.GetProgramList;
using CourseCatalog.App.Features.Programs.Commands.CreateProgramCredential;
using CourseCatalog.App.Features.Programs.Commands.DeleteProgramCredential;
using CourseCatalog.App.Features.Programs.Commands.UpdateProgram;
using CourseCatalog.App.Features.Programs.Queries.GetProgramDetail;
using CourseCatalog.App.Features.Programs.Queries.GetProgramSummary;
using CourseCatalog.App.Filters;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using CourseCatalog.App.Features.Programs.Commands.CreateProgram;

namespace CourseCatalog.App.Controllers.API
{
    [RoutePrefix("api/programs")]
    public class ProgramsController : ApiController
    {
        private readonly IMediator _mediator;

        public ProgramsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var dtos = await _mediator.Send(new GetProgramListQuery());
            return Ok(dtos);
        }

        [HttpGet, Route("{programId}")]
        public async Task<IHttpActionResult> Get(int programId)
        {
            var dto = await _mediator.Send(new GetProgramDetailQuery(programId));
            return Ok(dto);
        }

        [HttpPut, Route("")]
        public async Task<IHttpActionResult> UpdateProgram([FromBody] UpdateProgramCommand updateProgramCommand)
        {
            var id = await _mediator.Send(updateProgramCommand);
            return Ok(id);
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> CreateProgram([FromBody] CreateProgramCommand createProgramCommand)
        {
            var id = await _mediator.Send(createProgramCommand);
            return Ok(id);
        }

        [HttpDelete, CustomAuthorize(Roles = "CareerTechAdmin, Admin")]
        [Route("{programId}/credentials/{credentialId}")]
        public async Task<IHttpActionResult> DeleteProgramCredential(int programId, int credentialId)
        {
            await _mediator.Send(new DeleteProgramCredentialCommand(programId, credentialId));
            return Ok();
        }

        [HttpPost]
        [Route("credentials"), CustomAuthorize(Roles = "CareerTechAdmin, Admin")]
        public async Task<IHttpActionResult> CreateProgramCredential([FromBody] CreateProgramCredentialCommand createProgramCredentialCommand)
        {
            var dto = await _mediator.Send(createProgramCredentialCommand);
            return Ok(dto);
        }

        [HttpGet, Route("summary")]
        public async Task<object> Summary()
        {
            var dto = await _mediator.Send(new GetProgramSummaryQuery());
            return Ok(dto);
        }
    }
}

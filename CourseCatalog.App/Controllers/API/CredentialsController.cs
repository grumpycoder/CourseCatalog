using System.Threading.Tasks;
using System.Web.Http;
using CourseCatalog.App.Features.Credentials.Commands.CreateCredential;
using CourseCatalog.App.Features.Credentials.Commands.CreateCredentialProgram;
using CourseCatalog.App.Features.Credentials.Commands.DeleteCredentialProgram;
using CourseCatalog.App.Features.Credentials.Commands.UpdateCredential;
using CourseCatalog.App.Features.Credentials.Queries.GetCredentialDetail;
using CourseCatalog.App.Features.Credentials.Queries.GetCredentialList;
using CourseCatalog.App.Filters;
using MediatR;

namespace CourseCatalog.App.Controllers.API
{
    [RoutePrefix("api/credentials")]
    public class CredentialsController : ApiController
    {
        private readonly IMediator _mediator;

        public CredentialsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var dtos = await _mediator.Send(new GetCredentialListQuery());
            return Ok(dtos);
        }

        [HttpGet]
        [Route("{credentialId}")]
        public async Task<IHttpActionResult> Get(int credentialId)
        {
            var dto = await _mediator.Send(new GetCredentialDetailQuery(credentialId));
            return Ok(dto);
        }

        [HttpPut]
        [Route]
        [CustomAuthorize(Roles = "CareerTechAdmin, Admin")]
        public async Task<IHttpActionResult> UpdateCredential(
            [FromBody] UpdateCredentialCommand updateCredentialCommand)
        {
            var id = await _mediator.Send(updateCredentialCommand);
            return Ok(id);
        }

        [HttpPost]
        [Route]
        [CustomAuthorize(Roles = "CareerTechAdmin, Admin")]
        public async Task<IHttpActionResult> CreateCredential(
            [FromBody] CreateCredentialCommand createCredentialCommand)
        {
            var id = await _mediator.Send(createCredentialCommand);
            return Ok(id);
        }

        [HttpDelete]
        [Route("{credentialId}/programs/{programId}")]
        [CustomAuthorize(Roles = "CareerTechAdmin, Admin")]
        public async Task<IHttpActionResult> DeleteProgramCredential(int credentialId, int programId)
        {
            await _mediator.Send(new DeleteCredentialProgramCommand(programId, credentialId));
            return Ok();
        }

        [HttpPost]
        [Route("programs")]
        [CustomAuthorize(Roles = "CareerTechAdmin, Admin")]
        public async Task<IHttpActionResult> CreateCredentialProgram(
            [FromBody] CreateCredentialProgramCommand createCredentialProgramCommand)
        {
            var dto = await _mediator.Send(createCredentialProgramCommand);
            return Ok(dto);
        }
    }
}
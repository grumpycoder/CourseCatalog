using CourseCatalog.App.Features.Drafts.Commands.Create;
using CourseCatalog.App.Features.Drafts.Commands.CreateDraftProgram;
using CourseCatalog.App.Features.Drafts.Commands.CreateDraftRequirement;
using CourseCatalog.App.Features.Drafts.Commands.DeleteDraftProgram;
using CourseCatalog.App.Features.Drafts.Commands.DeleteRequirement;
using CourseCatalog.App.Features.Drafts.Commands.UpdateDraft;
using CourseCatalog.App.Features.Drafts.Queries.GetDraftDetail;
using CourseCatalog.Domain.Entities;
using CourseCatalog.Persistence;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using MediatR;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

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

        [HttpPut, Route()]
        public async Task<IHttpActionResult> Put([FromBody] UpdateDraftCommand updateDraftCommand)
        {
            var dto = await _mediator.Send(updateDraftCommand);
            return Ok();
        }

        [HttpPost, Route()]
        public async Task<IHttpActionResult> Post([FromBody] CreateDraftCommand createDraftCommand)
        {
            var dto = await _mediator.Send(createDraftCommand);
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
    }
}

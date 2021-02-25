using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CourseCatalog.App.Features.Lookups.Queries.GetProgramList;
using MediatR;

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

    }
}

﻿using CourseCatalog.App.Features.Credentials.Queries.GetCredentialList;
using CourseCatalog.App.Features.Lookups.Queries.GetClusterTypetList;
using CourseCatalog.App.Features.Lookups.Queries.GetCourseLevelList;
using CourseCatalog.App.Features.Lookups.Queries.GetCreditTypeList;
using CourseCatalog.App.Features.Lookups.Queries.GetDeliveryTypeList;
using CourseCatalog.App.Features.Lookups.Queries.GetEndorsementList;
using CourseCatalog.App.Features.Lookups.Queries.GetGradeList;
using CourseCatalog.App.Features.Lookups.Queries.GetGradeScaleList;
using CourseCatalog.App.Features.Lookups.Queries.GetProgramList;
using CourseCatalog.App.Features.Lookups.Queries.GetProgramTypeList;
using CourseCatalog.App.Features.Lookups.Queries.GetScedCategoryList;
using CourseCatalog.App.Features.Lookups.Queries.GetSchoolYearList;
using CourseCatalog.App.Features.Lookups.Queries.GetSubjectList;
using CourseCatalog.App.Features.Lookups.Queries.GetTagList;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;

namespace CourseCatalog.App.Controllers.API
{
    [RoutePrefix("api/refs")]
    public class RefsController : ApiController
    {
        private readonly IMediator _mediator;

        public RefsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet, Route("schoolyears")]
        public async Task<IHttpActionResult> SchoolYears()
        {
            var dto = await _mediator.Send(new GetSchoolYearListQuery());
            return Ok(dto);
        }

        [HttpGet, Route("grades")]
        public async Task<IHttpActionResult> Grades()
        {
            var dto = await _mediator.Send(new GetGradeListQuery());
            return Ok(dto);
        }

        [HttpGet, Route("gradescales")]
        public async Task<IHttpActionResult> GradeScales()
        {
            var dto = await _mediator.Send(new GetGradeScaleListQuery());
            return Ok(dto);
        }

        [HttpGet, Route("subjects")]
        public async Task<IHttpActionResult> Subjects()
        {
            var dto = await _mediator.Send(new GetSubjectListQuery());
            return Ok(dto);
        }

        [HttpGet, Route("scedcategories")]
        public async Task<IHttpActionResult> ScedCategories()
        {
            var dto = await _mediator.Send(new GetScedCategoryListQuery());
            return Ok(dto);
        }

        [HttpGet, Route("courselevels")]
        public async Task<IHttpActionResult> CourseLevels()
        {
            var dto = await _mediator.Send(new GetCourseLevelListQuery());
            return Ok(dto);
        }

        [HttpGet, Route("credittypes")]
        public async Task<IHttpActionResult> CreditTypes()
        {
            var dto = await _mediator.Send(new GetCreditTypeListQuery());
            return Ok(dto);
        }

        [HttpGet, Route("tags")]
        public async Task<IHttpActionResult> Tags()
        {
            var dto = await _mediator.Send(new GetTagListQuery());
            return Ok(dto);
        }

        [HttpGet, Route("deliverytypes")]
        public async Task<IHttpActionResult> DeliveryTypes()
        {
            var dto = await _mediator.Send(new GetDeliveryTypeListQuery());
            return Ok(dto);
        }

        [HttpGet, Route("endorsements")]
        public async Task<IHttpActionResult> Endorsements()
        {
            var dto = await _mediator.Send(new GetEndorsementListQuery());
            return Ok(dto);
        }

        [HttpGet, Route("programs")]
        public async Task<IHttpActionResult> Programs()
        {
            var dto = await _mediator.Send(new GetProgramListQuery());
            return Ok(dto);
        }

        [HttpGet, Route("clusterTypes")]
        public async Task<IHttpActionResult> ClusterTypes()
        {
            var dto = await _mediator.Send(new GetClusterTypeListQuery());
            return Ok(dto);
        }

        [HttpGet, Route("programTypes")]
        public async Task<IHttpActionResult> ProgramTypes()
        {
            var dto = await _mediator.Send(new GetProgramTypeListQuery());
            return Ok(dto);
        }

        [HttpGet, Route("credentialTypes")]
        public async Task<IHttpActionResult> CredentialTypes()
        {
            var dto = await _mediator.Send(new GetCredentialListQuery());
            return Ok(dto);
        }
    }

}

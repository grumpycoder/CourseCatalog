﻿using CourseCatalog.App.Features.Lookups.Queries.GetCipCodeList;
using CourseCatalog.App.Features.Lookups.Queries.GetClusterTypeList;
using CourseCatalog.App.Features.Lookups.Queries.GetCourseLevelList;
using CourseCatalog.App.Features.Lookups.Queries.GetCredentialTypeList;
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
using CourseCatalog.App.Features.Refs.ClusterTypes.Commands.CreateClusterType;
using CourseCatalog.App.Features.Refs.ClusterTypes.Commands.DeleteClusterType;
using CourseCatalog.App.Features.Refs.ClusterTypes.Commands.UpdateClusterType;
using CourseCatalog.App.Features.Refs.CourseLevels.Commands.CreateCourseLevel;
using CourseCatalog.App.Features.Refs.CourseLevels.Commands.DeleteCourseLevel;
using CourseCatalog.App.Features.Refs.CourseLevels.Commands.UpdateCourseLevel;
using CourseCatalog.App.Features.Refs.CredentialTypes.Commands.CreateCredentialType;
using CourseCatalog.App.Features.Refs.CredentialTypes.Commands.DeleteCredentialType;
using CourseCatalog.App.Features.Refs.CredentialTypes.Commands.UpdateCredentialType;
using CourseCatalog.App.Features.Refs.CreditTypes.Commands.CreateCreditType;
using CourseCatalog.App.Features.Refs.CreditTypes.Commands.DeleteCreditType;
using CourseCatalog.App.Features.Refs.CreditTypes.Commands.UpdateCreditType;
using CourseCatalog.App.Features.Refs.DeliveryTypes.Commands.CreateDeliveryType;
using CourseCatalog.App.Features.Refs.DeliveryTypes.Commands.DeleteDeliveryType;
using CourseCatalog.App.Features.Refs.DeliveryTypes.Commands.UpdateDeliveryType;
using CourseCatalog.App.Features.Refs.ProgramTypes.Commands.CreateProgramType;
using CourseCatalog.App.Features.Refs.ProgramTypes.Commands.DeleteProgramType;
using CourseCatalog.App.Features.Refs.ProgramTypes.Commands.UpdateProgramType;
using CourseCatalog.App.Features.Refs.Subjects.Commands;
using CourseCatalog.App.Filters;
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

        [HttpGet]
        [Route("schoolYears")]
        public async Task<IHttpActionResult> SchoolYears()
        {
            var dto = await _mediator.Send(new GetSchoolYearListQuery());
            return Ok(dto);
        }

        [HttpGet]
        [Route("grades")]
        public async Task<IHttpActionResult> Grades()
        {
            var dto = await _mediator.Send(new GetGradeListQuery());
            return Ok(dto);
        }

        [HttpGet]
        [Route("gradeScales")]
        public async Task<IHttpActionResult> GradeScales()
        {
            var dto = await _mediator.Send(new GetGradeScaleListQuery());
            return Ok(dto);
        }

        [HttpGet]
        [Route("subjects")]
        public async Task<IHttpActionResult> Subjects()
        {
            var dto = await _mediator.Send(new GetSubjectListQuery());
            return Ok(dto);
        }

        [HttpPut]
        [Route("subjects")]
        public async Task<IHttpActionResult> Subjects([FromBody] UpdateSubjectCommand updateSubjectCommand)
        {
            var id = await _mediator.Send(updateSubjectCommand);
            return Ok(id);
        }

        [HttpPost]
        [Route("subjects")]
        public async Task<IHttpActionResult> Subjects([FromBody] CreateSubjectCommand createSubjectCommand)
        {
            var id = await _mediator.Send(createSubjectCommand);
            return Ok(id);
        }

        [HttpDelete]
        [Route("subjects/{subjectId}")]
        public async Task<IHttpActionResult> Subjects(int subjectId)
        {
            await _mediator.Send(new DeleteSubjectCommand(subjectId));
            return Ok();
        }

        [HttpGet]
        [Route("scedCategories")]
        public async Task<IHttpActionResult> ScedCategories()
        {
            var dto = await _mediator.Send(new GetScedCategoryListQuery());
            return Ok(dto);
        }

        [HttpGet]
        [Route("courseLevels")]
        public async Task<IHttpActionResult> CourseLevels()
        {
            var dto = await _mediator.Send(new GetCourseLevelListQuery());
            return Ok(dto);
        }

        [HttpPut]
        [Route("courseLevels")]
        [CustomAuthorize(Roles = "CourseAdmin, Admin")]
        public async Task<IHttpActionResult> CourseLevels([FromBody] UpdateCourseLevelCommand updateCourseLevelCommand)
        {
            var id = await _mediator.Send(updateCourseLevelCommand);
            return Ok(id);
        }

        [HttpPost]
        [Route("courseLevels")]
        [CustomAuthorize(Roles = "CourseAdmin, Admin")]
        public async Task<IHttpActionResult> CourseLevels([FromBody] CreateCourseLevelCommand createCourseLevelCommand)
        {
            var id = await _mediator.Send(createCourseLevelCommand);
            return Ok(id);
        }

        [HttpDelete]
        [Route("courseLevels/{courseLevelId}")]
        [CustomAuthorize(Roles = "CourseAdmin, Admin")]
        public async Task<IHttpActionResult> CourseLevels(int courseLevelId)
        {
            await _mediator.Send(new DeleteCourseLevelCommand(courseLevelId));
            return Ok();
        }

        [HttpGet]
        [Route("creditTypes")]
        [CustomAuthorize(Roles = "CourseAdmin, Admin")]
        public async Task<IHttpActionResult> CreditTypes()
        {
            var dto = await _mediator.Send(new GetCreditTypeListQuery());
            return Ok(dto);
        }

        [HttpPut]
        [Route("creditTypes")]
        [CustomAuthorize(Roles = "CourseAdmin, Admin")]
        public async Task<IHttpActionResult> CreditTypes([FromBody] UpdateCreditTypeCommand updateCreditTypeCommand)
        {
            var id = await _mediator.Send(updateCreditTypeCommand);
            return Ok(id);
        }

        [HttpPost]
        [Route("creditTypes")]
        [CustomAuthorize(Roles = "CourseAdmin, Admin")]
        public async Task<IHttpActionResult> CreditTypes([FromBody] CreateCreditTypeCommand createCreditTypeCommand)
        {
            var id = await _mediator.Send(createCreditTypeCommand);
            return Ok(id);
        }

        [HttpDelete]
        [Route("creditTypes/{tagId}")]
        [CustomAuthorize(Roles = "CourseAdmin, Admin")]
        public async Task<IHttpActionResult> CreditTypes(int tagId)
        {
            await _mediator.Send(new DeleteCreditTypeCommand(tagId));
            return Ok();
        }

        [HttpGet]
        [Route("tags")]
        public async Task<IHttpActionResult> Tags()
        {
            var dto = await _mediator.Send(new GetTagListQuery());
            return Ok(dto);
        }

        [HttpGet]
        [Route("deliveryTypes")]
        public async Task<IHttpActionResult> DeliveryTypes()
        {
            var dto = await _mediator.Send(new GetDeliveryTypeListQuery());
            return Ok(dto);
        }

        [HttpPut]
        [Route("deliveryTypes")]
        [CustomAuthorize(Roles = "CourseAdmin, Admin")]
        public async Task<IHttpActionResult> DeliveryTypes(
            [FromBody] UpdateDeliveryTypeCommand updateDeliveryTypeCommand)
        {
            var id = await _mediator.Send(updateDeliveryTypeCommand);
            return Ok(id);
        }

        [HttpPost]
        [Route("deliveryTypes")]
        [CustomAuthorize(Roles = "CourseAdmin, Admin")]
        public async Task<IHttpActionResult> DeliveryTypes(
            [FromBody] CreateDeliveryTypeCommand createDeliveryTypeCommand)
        {
            var id = await _mediator.Send(createDeliveryTypeCommand);
            return Ok(id);
        }

        [HttpDelete]
        [Route("deliveryTypes/{deliveryTypeId}")]
        [CustomAuthorize(Roles = "CourseAdmin, Admin")]
        public async Task<IHttpActionResult> DeliveryTypes(int deliveryTypeId)
        {
            await _mediator.Send(new DeleteDeliveryTypeCommand(deliveryTypeId));
            return Ok();
        }

        [HttpGet]
        [Route("endorsements")]
        public async Task<IHttpActionResult> Endorsements()
        {
            var dto = await _mediator.Send(new GetEndorsementListQuery());
            return Ok(dto);
        }

        [HttpGet]
        [Route("programs")]
        public async Task<IHttpActionResult> Programs()
        {
            var dto = await _mediator.Send(new GetProgramListQuery());
            return Ok(dto);
        }

        [HttpGet]
        [Route("clusterTypes")]
        public async Task<IHttpActionResult> ClusterTypes()
        {
            var dto = await _mediator.Send(new GetClusterTypeListQuery());
            return Ok(dto);
        }

        [HttpPut]
        [Route("clusterTypes")]
        [CustomAuthorize(Roles = "CourseAdmin, Admin, CareerTechAdmin")]
        public async Task<IHttpActionResult> ClusterTypes([FromBody] UpdateClusterTypeCommand updateClusterTypeCommand)
        {
            var id = await _mediator.Send(updateClusterTypeCommand);
            return Ok(id);
        }

        [HttpPost]
        [Route("clusterTypes")]
        [CustomAuthorize(Roles = "CourseAdmin, Admin, CareerTechAdmin")]
        public async Task<IHttpActionResult> ClusterTypes([FromBody] CreateClusterTypeCommand createClusterTypeCommand)
        {
            var id = await _mediator.Send(createClusterTypeCommand);
            return Ok(id);
        }

        [HttpDelete]
        [Route("clusterTypes/{clusterTypeId}")]
        [CustomAuthorize(Roles = "CourseAdmin, Admin, CareerTechAdmin")]
        public async Task<IHttpActionResult> ClusterTypes(int clusterTypeId)
        {
            await _mediator.Send(new DeleteClusterTypeCommand(clusterTypeId));
            return Ok();
        }

        [HttpGet]
        [Route("programTypes")]
        public async Task<IHttpActionResult> ProgramTypes()
        {
            var dto = await _mediator.Send(new GetProgramTypeListQuery());
            return Ok(dto);
        }

        [HttpPut]
        [Route("programTypes")]
        [CustomAuthorize(Roles = "CourseAdmin, Admin, CareerTechAdmin")]
        public async Task<IHttpActionResult> ProgramTypes([FromBody] UpdateProgramTypeCommand updateProgramTypeCommand)
        {
            var id = await _mediator.Send(updateProgramTypeCommand);
            return Ok(id);
        }

        [HttpPost]
        [Route("programTypes")]
        [CustomAuthorize(Roles = "CourseAdmin, Admin, CareerTechAdmin")]
        public async Task<IHttpActionResult> ProgramTypes([FromBody] CreateProgramTypeCommand createProgramTypeCommand)
        {
            var id = await _mediator.Send(createProgramTypeCommand);
            return Ok(id);
        }

        [HttpDelete]
        [Route("programTypes/{programTypeId}")]
        [CustomAuthorize(Roles = "CourseAdmin, Admin, CareerTechAdmin")]
        public async Task<IHttpActionResult> ProgramTypes(int programTypeId)
        {
            await _mediator.Send(new DeleteProgramTypeCommand(programTypeId));
            return Ok();
        }

        [HttpGet]
        [Route("credentialTypes")]
        public async Task<IHttpActionResult> CredentialTypes()
        {
            var dto = await _mediator.Send(new GetCredentialTypeListQuery());
            return Ok(dto);
        }

        [HttpPut]
        [Route("credentialTypes")]
        [CustomAuthorize(Roles = "CourseAdmin, Admin, CareerTechAdmin")]
        public async Task<IHttpActionResult> CredentialTypes(
            [FromBody] UpdateCredentialTypeCommand updateCredentialTypeCommand)
        {
            var id = await _mediator.Send(updateCredentialTypeCommand);
            return Ok(id);
        }

        [HttpPost]
        [Route("credentialTypes")]
        [CustomAuthorize(Roles = "CourseAdmin, Admin, CareerTechAdmin")]
        public async Task<IHttpActionResult> CredentialTypes(
            [FromBody] CreateCredentialTypeCommand createCredentialTypeCommand)
        {
            var id = await _mediator.Send(createCredentialTypeCommand);
            return Ok(id);
        }

        [HttpDelete]
        [Route("credentialTypes/{credentialTypeId}")]
        [CustomAuthorize(Roles = "CourseAdmin, Admin, CareerTechAdmin")]
        public async Task<IHttpActionResult> CredentialTypes(int credentialTypeId)
        {
            await _mediator.Send(new DeleteCredentialTypeCommand(credentialTypeId));
            return Ok();
        }

        [HttpGet]
        [Route("cipCodes")]
        public async Task<IHttpActionResult> CipCodes()
        {
            var dto = await _mediator.Send(new GetCipCodeListQuery());
            return Ok(dto);
        }
    }
}
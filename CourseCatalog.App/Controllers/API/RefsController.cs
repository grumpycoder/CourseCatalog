using CourseCatalog.App.Features.Lookups.Queries.GetCourseLevelList;
using CourseCatalog.App.Features.Lookups.Queries.GetCreditTypeList;
using CourseCatalog.App.Features.Lookups.Queries.GetDeliveryTypeList;
using CourseCatalog.App.Features.Lookups.Queries.GetEndorsementList;
using CourseCatalog.App.Features.Lookups.Queries.GetGradeList;
using CourseCatalog.App.Features.Lookups.Queries.GetGradeScaleList;
using CourseCatalog.App.Features.Lookups.Queries.GetScedCategoryList;
using CourseCatalog.App.Features.Lookups.Queries.GetSchoolYearList;
using CourseCatalog.App.Features.Lookups.Queries.GetSubjectList;
using CourseCatalog.App.Features.Lookups.Queries.GetTagList;
using CourseCatalog.Persistence;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using CourseCatalog.App.Features.Lookups.Queries.GetProgramList;

namespace CourseCatalog.App.Controllers.API
{
    [RoutePrefix("api/refs")]
    public class RefsController : ApiController
    {
        private readonly CourseDbContext _context;
        private readonly IMediator _mediator;

        public RefsController(CourseDbContext context, IMediator mediator)
        {
            _context = context;
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

    }

}

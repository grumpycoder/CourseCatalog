using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using CourseCatalog.App.Features.Courses.Queries.GetCourseDetail;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using CourseCatalog.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseCatalog.App.Controllers.API
{
    public class ValuesController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IAsyncRepository<Draft> _repository;
        private readonly CourseDbContext _context;

        //public ValuesController(CourseDbContext context)
        //{
        //    _context = context;
        //}

        //public ValuesController(IAsyncRepository<Draft> repository)
        //{
        //    _repository = repository;
        //}

        public ValuesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/values
        public async Task<IHttpActionResult> GetAsync()
        {
            //var dto = await _context.Courses
            //    .Include(c => c.ScedCategory)
            //    .Include(c => c.GradeScale)
            //    .Include(c => c.LowGrade)
            //    .Include(c => c.HighGrade)
            //    .Include(c => c.Subject)
            //    .Include(c => c.CourseLevel)
            //    .Include(c => c.Programs).ThenInclude(p => p.Program).ThenInclude(c => c.Cluster).ThenInclude(ct => ct.ClusterType)
            //    .Include(c => c.DeliveryTypes).ThenInclude(d => d.DeliveryType)
            //    .Include(c => c.Endorsements).ThenInclude(e => e.Endorsement)
            //    .FirstOrDefaultAsync(c => c.CourseId == 0);

            //var dto = await _context.Drafts
            //    .Include(c => c.ScedCategory)
            //    .Include(c => c.GradeScale)
            //    .Include(c => c.LowGrade)
            //    .Include(c => c.HighGrade)
            //    .Include(c => c.Subject)
            //    .Include(c => c.CourseLevel)
            //    .Include(c => c.Programs).ThenInclude(p => p.Program).ThenInclude(c => c.Cluster).ThenInclude(ct => ct.ClusterType)
            //    .Include(c => c.DeliveryTypes).ThenInclude(d => d.DeliveryType)
            //    .Include(c => c.Endorsements).ThenInclude(e => e.Endorsement)
            //    .FirstOrDefaultAsync(c => c.DraftId == 68);

            //var dto = await _repository.GetByIdAsync(68);

            var dto = await _mediator.Send(new GetCourseDetailQuery(0));

            return Ok(dto);

            return Ok(new string[] { "value1", "value2" });
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}

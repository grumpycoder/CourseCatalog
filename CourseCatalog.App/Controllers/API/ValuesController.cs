using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using CourseCatalog.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CourseCatalog.App.Controllers.API
{
    public class ValuesController : ApiController
    {
        private readonly CourseDbContext _context;

        public ValuesController(CourseDbContext context)
        {
            _context = context;
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

            var dto = await _context.Drafts
                .Include(c => c.ScedCategory)
                .Include(c => c.GradeScale)
                .Include(c => c.LowGrade)
                .Include(c => c.HighGrade)
                .Include(c => c.Subject)
                .Include(c => c.CourseLevel)
                .Include(c => c.Programs).ThenInclude(p => p.Program).ThenInclude(c => c.Cluster).ThenInclude(ct => ct.ClusterType)
                .Include(c => c.DeliveryTypes).ThenInclude(d => d.DeliveryType)
                .Include(c => c.Endorsements).ThenInclude(e => e.Endorsement)
                .FirstOrDefaultAsync(c => c.DraftId == 68);

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

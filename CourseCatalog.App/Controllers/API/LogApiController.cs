using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using CourseCatalog.App.Filters;
using CourseCatalog.Persistence;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseCatalog.App.Controllers.API
{
    [RoutePrefix("api/logs")]
    [CustomAuthorize(Roles = "CourseAdmin, Admin")]
    [DisplayName("Logging")]
    public class LogApiController : ApiController
    {
        private readonly CourseDbContext _context;

        public LogApiController(CourseDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("error")]
        public async Task<object> Error(DataSourceLoadOptions loadOptions)
        {
            var dto = await _context.ErrorLogs.ToListAsync();
            return Ok(DataSourceLoader.Load(dto.OrderByDescending(x => x.LogId), loadOptions));
        }

        [HttpGet]
        [Route("perf")]
        public async Task<object> Performance(DataSourceLoadOptions loadOptions)
        {
            var dto = await _context.PerformanceLogs.ToListAsync();
            return Ok(DataSourceLoader.Load(dto.OrderByDescending(x => x.Timestamp), loadOptions));
        }
    }
}
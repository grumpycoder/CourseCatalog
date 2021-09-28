using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCatalog.Persistence.Repositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(CourseDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> GetActiveCourseCount()
        {
            var count = await _dbContext.CoursesView.CountAsync(c => c.IsRetired == false);
            return count;
        }

        public async Task<Course> GetCourseByCourseNumber(string courseNumber)
        {
            var course = await _dbContext.Courses.FirstOrDefaultAsync(c => c.CourseNumber == courseNumber);

            return course;
        }

        public async Task<List<Course>> GetCoursesByEndorseId(int endorseId)
        {
            try
            {
                var courses = await _dbContext.Courses
                    .Include(c => c.Subject)
                    .Include(c => c.GradeScale)
                    .Include(c => c.ScedCategory)
                    .Include(c => c.CourseLevel)
                    .Include(c => c.LowGrade)
                    .Include(c => c.HighGrade)
                    .Where(c => c.Endorsements.Any(e => e.EndorsementId == endorseId))
                    .ToListAsync();
                return courses;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<List<Course>> GetCoursesByCertholderId(int certholderId)
        {
            var certificates = _dbContext.Certificates
                .Where(x => x.CertholderId == certholderId);

            var endorsements = certificates.Select(e => e.EndorsementId).ToList();

            var list = string.Join(",", endorsements.Select(n => n.ToString()));

            var query =
                $"select c.* from Common.Courses c join Common.CourseEndorsements ce on ce.CourseId = c.CourseId where ce.EndorseId IN ({list})";

            return await _dbContext.Courses.FromSqlRaw(query).ToListAsync();
        }

        public async Task<Course> GetCourseByIdWithDetails(int courseId)
        {
            var course = await _dbContext.Courses
                .Include(c => c.CourseLevel)
                .Include(c => c.Subject)
                .Include(c => c.HighGrade)
                .Include(c => c.LowGrade)
                .Include(c => c.GradeScale)
                .Include(c => c.ScedCategory)
                .Include(c => c.DeliveryTypes).ThenInclude(d => d.DeliveryType)
                .Include(c => c.Endorsements).ThenInclude(e => e.Endorsement)
                .Include(c => c.Programs).ThenInclude(e => e.Program)
                .FirstOrDefaultAsync(x => x.CourseId == courseId);

            return course;
        }

        public async Task<string> GetCourseXml(int schoolYear)
        {

            var constr = _dbContext.Database.GetDbConnection().ConnectionString;
            string xml = string.Empty;
            var sb = new StringBuilder();

            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = $"EXEC Common.p_getSTI_Courses @SchoolYear = {schoolYear}";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (sdr.Read())
                        {

                            xml = sdr[0].ToString();
                            sb.Append(sdr[0]);
                        }
                    }
                    con.Close();
                    return sb.ToString();
                }
            }

        }

    }
}
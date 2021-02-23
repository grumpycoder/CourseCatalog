using System.Threading.Tasks;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseCatalog.Persistence.Repositories
{
    public class DraftRepository : BaseRepository<Draft>, IDraftRepository
    {
        public DraftRepository(CourseDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Draft> GetDraftByCourseNumber(string courseNumber)
        {
            var draft = await _dbContext.Drafts.FirstOrDefaultAsync(c => c.CourseNumber == courseNumber);

            return draft;
        }

        public async Task<Draft> GetDraftByIdWithDetails(int draftId)
        {
            var draft = await _dbContext.Drafts
                .Include(c => c.CourseLevel)
                .Include(c => c.Subject)
                .Include(c => c.HighGrade)
                .Include(c => c.LowGrade)
                .Include(c => c.GradeScale)
                .Include(c => c.ScedCategory)
                .Include(c => c.DeliveryTypes).ThenInclude(d => d.DeliveryType)
                .Include(c => c.Endorsements).ThenInclude(e => e.Endorsement)
                .Include(c => c.Programs).ThenInclude(e => e.Program)
                .FirstOrDefaultAsync(x => x.DraftId == draftId);

            return draft;
        }
    }
}
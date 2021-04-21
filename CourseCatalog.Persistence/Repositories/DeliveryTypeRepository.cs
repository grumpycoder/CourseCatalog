using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CourseCatalog.Persistence.Repositories
{
    public class DeliveryTypeRepository : BaseRepository<DeliveryType>, IDeliveryTypeRepository
    {
        public DeliveryTypeRepository(CourseDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<DeliveryType> GetDeliveryTypeByName(string name)
        {
            return await _dbContext.DeliveryTypes.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<bool> HasCourses(int deliveryTypeId)
        {
            return await _dbContext.Courses.AnyAsync(c => c.DeliveryTypes.Any(d => d.DeliveryTypeId == deliveryTypeId));
        }
    }
}
using CourseCatalog.Domain.Entities;
using System.Threading.Tasks;

namespace CourseCatalog.Application.Contracts
{
    public interface IDeliveryTypeRepository : IAsyncRepository<DeliveryType>
    {
        Task<DeliveryType> GetDeliveryTypeByName(string name);
        Task<bool> HasCourses(int deliveryTypeId);
    }
}
using System.Threading.Tasks;
using CourseCatalog.Domain.Entities;

namespace CourseCatalog.Application.Contracts
{
    public interface IDeliveryTypeRepository : IAsyncRepository<DeliveryType>
    {
        Task<DeliveryType> GetDeliveryTypeByName(string name);
        Task<bool> HasCourses(int deliveryTypeId);
    }
}
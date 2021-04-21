using System.Threading.Tasks;
using CourseCatalog.Application.Responses;
using CourseCatalog.Domain.Entities;

namespace CourseCatalog.Application.Contracts
{
    public interface IPublishCourseService
    {
        Task<BaseResponse> PublishCourse(Course course);
    }
}
using CourseCatalog.Application.Responses;
using CourseCatalog.Domain.Entities;
using System.Threading.Tasks;

namespace CourseCatalog.Application.Contracts
{
    public interface IPublishCourseService
    {
        Task<BaseResponse> PublishCourse(Course course);
    }

}

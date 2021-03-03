using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest
    {
        public User User { get; set; }

        public UpdateUserCommand(User user)
        {
            User = user;
        }
    }
}

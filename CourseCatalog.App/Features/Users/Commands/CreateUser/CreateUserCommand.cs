using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<User>
    {
        public User User { get; set; }

        public CreateUserCommand(User user)
        {
            User = user;
        }
    }
}

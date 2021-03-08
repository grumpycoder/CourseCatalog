using MediatR;
using System;

namespace CourseCatalog.App.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest
    {

        public int Id { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public Guid IdentityGuid { get; set; }

    }
}

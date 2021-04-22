using System;
using System.Threading;
using System.Threading.Tasks;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userToAdd = new User
            {
                EmailAddress = request.EmailAddress.ToLower(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                FullName = request.FullName,
                IdentityGuid = request.IdentityGuid,
                Username = request.Username.ToLower()
            };
            var user = await _userRepository.AddAsync(userToAdd);
            return user.IdentityGuid;
        }
    }
}
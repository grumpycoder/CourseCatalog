using System;
using System.Threading;
using System.Threading.Tasks;
using CourseCatalog.Application.Contracts;
using MediatR;

namespace CourseCatalog.App.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByIdentityGuidAsync(request.IdentityGuid);

                user.EmailAddress = request.EmailAddress.ToLower();
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.FullName = request.FullName;
                //user.Username = request.Username.ToLower();

                await _userRepository.UpdateAsync(user);

                return Unit.Value;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
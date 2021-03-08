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
            var user = await _userRepository.GetByIdAsync(request.User.IdentityGuid);

            user.EmailAddress = request.User.EmailAddress.ToLower(); 
            user.FirstName = request.User.FirstName; 
            user.LastName = request.User.LastName; 
            //user.FullName = request.User.FullName; 
            user.Username = request.User.Username.ToLower(); 

            await _userRepository.UpdateAsync(user);

            return Unit.Value;
        }
    }
}

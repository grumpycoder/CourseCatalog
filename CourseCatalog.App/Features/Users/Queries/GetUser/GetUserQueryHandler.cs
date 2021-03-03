using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Users.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
    {
        private readonly IUserRepository _userRepository;

        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdWithDetails(request.UserId);
            return user;
        }
    }
}

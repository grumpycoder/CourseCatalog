using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Users.Queries.GetUserGroupList
{
    public class GetUserGroupListQueryHandler : IRequestHandler<GetUserGroupListQuery, List<Group>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserGroupListQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<Group>> Handle(GetUserGroupListQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdWithDetails(request.UserId);
            return user.Groups.Select(userGroup => userGroup.Group).ToList();
        }
    }
}

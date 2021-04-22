using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Groups.Commands.DeleteGroupUser
{
    public class DeleteGroupUserCommandHandler : IRequestHandler<DeleteGroupUserCommand>
    {
        private readonly IGroupRepository _groupRepository;

        public DeleteGroupUserCommandHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<Unit> Handle(DeleteGroupUserCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetGroupByIdWithUsers(request.GroupId);

            if (group == null) throw new BadRequestException("Group does not exist");

            var user = group.Users.FirstOrDefault(u => u.User.IdentityGuid == request.IdentityGuid);

            if (user == null) throw new BadRequestException("User does not exist in group");

            group.Users.Remove(user);

            await _groupRepository.UpdateAsync(group);

            return Unit.Value;
        }
    }
}
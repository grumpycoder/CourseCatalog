using MediatR;
using System;

namespace CourseCatalog.App.Features.Groups.Commands.DeleteGroupUser
{
    public class DeleteGroupUserCommand : IRequest
    {
        public int GroupId { get; set; }
        public Guid IdentityGuid { get; set; }

        public DeleteGroupUserCommand(int groupId, Guid userId)
        {
            GroupId = groupId;
            IdentityGuid = userId;
        }
    }
}

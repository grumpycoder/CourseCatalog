using System;
using MediatR;

namespace CourseCatalog.App.Features.Groups.Commands.DeleteGroupUser
{
    public class DeleteGroupUserCommand : IRequest
    {
        public DeleteGroupUserCommand(int groupId, Guid userId)
        {
            GroupId = groupId;
            IdentityGuid = userId;
        }

        public int GroupId { get; set; }
        public Guid IdentityGuid { get; set; }
    }
}
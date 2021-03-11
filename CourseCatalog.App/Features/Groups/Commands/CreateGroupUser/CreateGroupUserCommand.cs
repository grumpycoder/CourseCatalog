﻿using MediatR;
using System;

namespace CourseCatalog.App.Features.Groups.Commands.CreateGroupUser
{
    public class CreateGroupUserCommand : IRequest<CreateGroupUserCommandDto>
    {
        public int GroupId { get; set; }
        public Guid IdentityGuid { get; set; }

        public CreateGroupUserCommand(int groupId, Guid identityGuid)
        {
            GroupId = groupId;
            IdentityGuid = identityGuid;
        }
    }

    public class CreateGroupUserCommandDto
    {
        public int GroupUserId { get; set; }
        public int GroupId { get; set; }
        public Guid IdentityGuid { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }

    }
}
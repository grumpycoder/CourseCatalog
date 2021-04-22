using MediatR;
using System;
using System.Collections.Generic;

namespace CourseCatalog.App.Features.Groups.Queries.GetGroupList
{
    public class GetGroupListQuery : IRequest<List<GroupListDto>>
    {
    }

    public class GroupListDto
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public List<UserDto> Users { get; set; }
    }

    public class UserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public Guid IdentityGuid { get; set; }
    }
}
using System;
using System.Collections.Generic;
using MediatR;

namespace CourseCatalog.App.Features.Users.Queries.GetUser
{
    public class GetUserQuery : IRequest<UserDetailDto>
    {
        public GetUserQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }
    }

    public class UserDetailDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public Guid IdentityGuid { get; set; }

        public List<UserGroupDto> Groups { get; set; }
    }

    public class UserGroupDto
    {
        public int GroupUserId { get; set; }
        public string GroupId { get; set; }
        public string GroupName { get; set; }
    }
}
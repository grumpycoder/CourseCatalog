using CourseCatalog.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;

namespace CourseCatalog.App.Features.Users.Queries.GetUserGroupList
{
    public class GetUserGroupListQuery : IRequest<List<Group>>
    {
        public Guid UserId { get; set; }
        public GetUserGroupListQuery(Guid userId)
        {
            UserId = userId;
        }
    }

    public class UserGroupListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

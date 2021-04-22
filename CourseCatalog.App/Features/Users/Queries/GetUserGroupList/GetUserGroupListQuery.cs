using System;
using System.Collections.Generic;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Users.Queries.GetUserGroupList
{
    public class GetUserGroupListQuery : IRequest<List<Group>>
    {
        public GetUserGroupListQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }
    }

    public class UserGroupListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
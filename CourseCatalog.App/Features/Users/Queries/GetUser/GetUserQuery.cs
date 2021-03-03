using CourseCatalog.Domain.Entities;
using MediatR;
using System;

namespace CourseCatalog.App.Features.Users.Queries.GetUser
{
    public class GetUserQuery : IRequest<User>
    {
        public Guid UserId { get; set; }

        public GetUserQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}

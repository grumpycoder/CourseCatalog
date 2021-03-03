using Alsde.Extensions;
using CourseCatalog.App.Features.Users.Commands.CreateUser;
using CourseCatalog.App.Features.Users.Commands.UpdateUser;
using CourseCatalog.App.Features.Users.Queries.GetUser;
using CourseCatalog.App.Features.Users.Queries.GetUserGroupList;
using CourseCatalog.App.Helpers;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CourseCatalog.App.Services
{

    public class MemberService : IMemberService
    {
        private readonly IMediator _mediator;

        public MemberService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<List<Group>> GetUserGroups(Guid identityGuid)
        {
            var groups = await _mediator.Send(new GetUserGroupListQuery(identityGuid));
            return groups;
        }

        public async Task SyncClaims(ClaimsIdentity identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            //Update local user attributes that may have changed since last login
            var username = identity.GetClaimValue(ClaimTypes.Email).Split('@')[0].ToLower();
            var emailAddress = identity.GetClaimValue(ClaimTypes.Email).ToLower();
            var lastName = identity.GetClaimValue(ClaimTypes.Surname).ToTitleCase();
            var firstName = identity.GetClaimValue(ClaimTypes.GivenName).ToTitleCase();
            var fullName = identity.GetClaimValue("FullName").ToLower().ToTitleCase();
            var identityGuid = new Guid(identity.GetClaimValue(ClaimTypes.NameIdentifier));
            var alsdeId = identity.GetClaimValue("AlsdeId");

            var user = await _mediator.Send(new GetUserQuery(identityGuid));

            switch (user)
            {
                case null:
                    user = new User(username, emailAddress, firstName, lastName, fullName, identityGuid);
                    await _mediator.Send(new CreateUserCommand(user));
                    break;
                default:
                    user.Update(username, emailAddress, firstName, lastName, fullName, identityGuid);
                    await _mediator.Send(new UpdateUserCommand(user));
                    break;
            }


            var guid = identity.GetClaimValue(ClaimTypes.NameIdentifier);
            var groups = await _mediator.Send(new GetUserGroupListQuery(new Guid(guid)));

            identity.AddGroupsToRoles(groups);
        }
    }


}

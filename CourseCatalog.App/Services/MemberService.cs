﻿using Alsde.Extensions;
using CourseCatalog.App.Features.Users.Commands.UpdateUser;
using CourseCatalog.App.Features.Users.Queries.GetUser;
using CourseCatalog.App.Features.Users.Queries.GetUserGroupList;
using CourseCatalog.App.Helpers;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
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
            //Update local user attributes that may have changed since last login
            var username = identity.GetClaimValue(ClaimTypes.Email).Split('@')[0].ToLower();
            var emailAddress = identity.GetClaimValue(ClaimTypes.Email).ToLower();
            var lastName = identity.GetClaimValue(ClaimTypes.Surname).ToTitleCase();
            var firstName = identity.GetClaimValue(ClaimTypes.GivenName).ToTitleCase();
            var fullName = identity.GetClaimValue("FullName").ToLower().ToTitleCase();
            var alsdeId = identity.GetClaimValue("AlsdeId");
            var identityGuid = new Guid(identity.GetClaimValue(ClaimTypes.NameIdentifier));

            try
            {
                var user = await _mediator.Send(new GetUserQuery(identityGuid));

                switch (user)
                {
                    case null:
                        break;
                    default:
                        await _mediator.Send(new UpdateUserCommand
                        {
                            EmailAddress = emailAddress,
                            FirstName = firstName,
                            LastName = lastName,
                            FullName = fullName,
                            IdentityGuid = identityGuid,
                            Username = username
                        });
                        break;
                }

                var groups = await _mediator.Send(new GetUserGroupListQuery(identityGuid));

                identity.AddGroupsToRoles(groups);
            }
            catch (Exception e)
            {
                if (e.GetType() != typeof(NotFoundException)) throw;
            }
        }
    }
}
using CourseCatalog.App.Features.Groups.Commands.CreateGroupUser;
using CourseCatalog.App.Features.Groups.Commands.DeleteGroupUser;
using CourseCatalog.App.Features.Groups.Queries.GetGroupList;
using CourseCatalog.App.Features.Users.Commands.CreateUser;
using CourseCatalog.App.Features.Users.Queries.GetUser;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Persistence;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Web.Http;

namespace CourseCatalog.App.Controllers.API
{
    [RoutePrefix("api/membership")]
    [DisplayName("Membership")]
    public class MembershipApiController : ApiController
    {
        private readonly CourseDbContext _context;
        private readonly IdemContext _idemContext;
        private readonly IMediator _mediator;
        private readonly ILoggedInUserService _loggedInUserService;

        public MembershipApiController(CourseDbContext context, IdemContext idemContext, IMediator mediator, ILoggedInUserService loggedInUserService)
        {
            _context = context;
            _idemContext = idemContext;
            _mediator = mediator;
            _loggedInUserService = loggedInUserService;
        }

        [HttpGet, Route("idem")]
        public async Task<IHttpActionResult> GetIdemUsers([FromUri] DataSourceLoadOptions loadOptions)
        {
            return Ok(await DataSourceLoader.LoadAsync(_idemContext.Users, loadOptions));
        }

        [HttpGet, Route("groups")]
        public async Task<IHttpActionResult> Groups()
        {
            var dtos = await _mediator.Send(new GetGroupListQuery());
            return Ok(dtos);
        }

        [HttpGet, Route("currentuser")]
        public async Task<IHttpActionResult> GetUserDetails()
        {
            var dto = await _mediator.Send(new GetUserQuery(_loggedInUserService.IdentityGuid));
            return Ok(dto);
        }


        [HttpDelete, Route("groups/{groupId}/user/{userId}"), Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> DeleteGroupMember(int groupId, Guid userId)
        {
            var dto = await _mediator.Send(new DeleteGroupUserCommand(groupId, userId));
            return Ok(dto);
        }

        [HttpPost, Route("groups/{groupId}/user/{identityGuid}"), Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> AddGroupMember(int groupId, Guid identityGuid)
        {

            UserDetailDto user = null;
            try
            {
                user = await _mediator.Send(new GetUserQuery(identityGuid));
            }
            catch (Exception e)
            {
                if (e.GetType() != typeof(NotFoundException))
                    throw;
            }

            if (user == null)
            {
                var idemUser = await _idemContext.Users.FirstOrDefaultAsync(i => i.IdentityGuid == identityGuid);
                if (idemUser == null) return BadRequest("Idem User not found.");
                var createUserCommand = new CreateUserCommand
                {
                    IdentityGuid = idemUser.IdentityGuid,
                    EmailAddress = idemUser.EmailAddress,
                    FirstName = idemUser.FirstName,
                    LastName = idemUser.LastName,
                    FullName = idemUser.FullName,
                    Username = idemUser.EmailAddress
                };
                await _mediator.Send(createUserCommand);
            }

            var dto = await _mediator.Send(new CreateGroupUserCommand(groupId, identityGuid));
            return Ok(dto);

        }

    }
}

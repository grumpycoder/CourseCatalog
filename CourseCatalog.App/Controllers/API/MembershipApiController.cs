using CourseCatalog.App.Features.Groups.Commands.CreateGroupUser;
using CourseCatalog.App.Features.Groups.Commands.DeleteGroupUser;
using CourseCatalog.App.Features.Groups.Queries.GetGroupList;
using CourseCatalog.Persistence;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using MediatR;
using System;
using System.ComponentModel;
using System.Linq;
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

        public MembershipApiController(CourseDbContext context, IdemContext idemContext, IMediator mediator)
        {
            _context = context;
            _idemContext = idemContext;
            _mediator = mediator;
        }

        [HttpGet, Route("users")]
        public object Users() => Ok(GetUsers());

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

        [HttpDelete, Route("groups/{groupId}/user/{userId}"), Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> DeleteGroupMember(int groupId, Guid userId)
        {
            var dto = await _mediator.Send(new DeleteGroupUserCommand(groupId, userId));
            return Ok(dto);
        }

        [HttpPost, Route("groups/{groupId}/user/{identityGuid}"), Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> AddGroupMember(int groupId, Guid identityGuid)
        {
            var dto = await _mediator.Send(new CreateGroupUserCommand(groupId, identityGuid));
            return Ok(dto);

            //if (user == null)
            //{
            //    //no local user record check idem for user
            //    user = await _idemContext.Users.FirstOrDefaultAsync(x => x.IdentityGuid == identityGuid);
            //    if (user == null) return BadRequest("User does not exist");

            //    //create new user record from idem
            //    user = new User(user.Username, user.EmailAddress, user.FirstName, user.LastName, user.FullName, user.IdentityGuid);
            //    _context.Attach(user);
            //    await _context.SaveChangesAsync();
            //}

            //var group = await _context.Groups.Include(u => u.UserGroups).FirstOrDefaultAsync(g => g.Id == groupId);
            //if (group == null) return BadRequest("Group does not exist");

            //try
            //{
            //    group.AddUser(user);
            //    _context.Attach(group);
            //    await _context.SaveChangesAsync();
            //    return Ok();
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.InnerException?.Message);
            //}
        }

        //[HttpPost, Route("groups/{groupName}"), Authorize(Roles = "Admin")]
        //public async Task<IHttpActionResult> CreateGroup(string groupName)
        //{
        //    var group = await _context.Groups.FirstOrDefaultAsync(g => g.Name == groupName);

        //    if (group != null) return BadRequest("Group already exists");

        //    try
        //    {
        //        group = new Group(groupName);

        //        _context.Attach(group);

        //        await _context.SaveChangesAsync();

        //        return Ok(group);
        //    }
        //    catch (Exception exception)
        //    {
        //        return BadRequest(exception.InnerException?.Message);
        //    }

        //}
        protected object GetUsers(string username = null)
        {
            var users = _context.Users.Where(x => x.Username.Contains(username) || username == null).ToList();
            return users;
        }


    }
}

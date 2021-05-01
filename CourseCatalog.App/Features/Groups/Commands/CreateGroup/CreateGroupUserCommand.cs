using MediatR;

namespace CourseCatalog.App.Features.Groups.Commands.CreateGroup
{
    public class CreateGroupCommand : IRequest<int>
    {
        public string GroupName { get; set; }

        public CreateGroupCommand(string groupName)
        {
            GroupName = groupName;
        }
    }

}
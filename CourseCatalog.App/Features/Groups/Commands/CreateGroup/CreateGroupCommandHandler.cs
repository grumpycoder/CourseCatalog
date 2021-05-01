using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Groups.Commands.CreateGroup
{
    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, int>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public CreateGroupCommandHandler(IMapper mapper, IGroupRepository groupRepository)
        {
            _mapper = mapper;
            _groupRepository = groupRepository;
        }

        public async Task<int> Handle(CreateGroupCommand request,
            CancellationToken cancellationToken)
        {

            var group = await _groupRepository.GetGroupByName(request.GroupName);

            if (group != null) throw new BadRequestException(
                $"Duplicate Group Name. {request.GroupName}");

            group = new Group() { Name = request.GroupName };

            await _groupRepository.AddAsync(group);

            return group.Id;
        }
    }
}
using AutoMapper;
using CourseCatalog.Application.Contracts;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Groups.Queries.GetGroupList
{
    public class GetGroupListQueryHandler : IRequestHandler<GetGroupListQuery, List<GroupListDto>>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public GetGroupListQueryHandler(IMapper mapper, IGroupRepository groupRepository)
        {
            _mapper = mapper;
            _groupRepository = groupRepository;
        }

        public async Task<List<GroupListDto>> Handle(GetGroupListQuery request, CancellationToken cancellationToken)
        {
            var groups = await _groupRepository.GetGroupsWithUsers();
            return _mapper.Map<List<GroupListDto>>(groups);
        }
    }
}
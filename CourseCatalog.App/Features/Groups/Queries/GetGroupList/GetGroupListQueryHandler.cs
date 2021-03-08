using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourseCatalog.Application.Contracts;
using MediatR;

namespace CourseCatalog.App.Features.Groups.Queries.GetGroupList
{
    public class GetGroupListQueryHandler: IRequestHandler<GetGroupListQuery, List<GroupListDto>>
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;

        public GetGroupListQueryHandler(IMapper mapper, IGroupRepository groupRepository)
        {
            _mapper = mapper;
            _groupRepository = groupRepository;
        }

        public async Task<List<GroupListDto>> Handle(GetGroupListQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var groups = await _groupRepository.GetGroupsWithUsers();
                return _mapper.Map<List<GroupListDto>>(groups);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}

using AutoMapper;
using CourseCatalog.Application.Contracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;

namespace CourseCatalog.App.Features.Users.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDetailDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetUserQueryHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserDetailDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdWithDetails(request.UserId);
            
            if(user == null) throw new NotFoundException(nameof(User), request.UserId);

            var dto = _mapper.Map<UserDetailDto>(user);
            return dto;
        }
    }
}

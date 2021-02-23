using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Drafts.Queries.GetDraftDetail
{

    public class GetDraftDetailQueryHandler : IRequestHandler<GetDraftDetailQuery, DraftDetailDto>
    {
        private readonly IDraftRepository _draftRepository;
        private readonly IMapper _mapper;

        public GetDraftDetailQueryHandler(IMapper mapper, IDraftRepository draftRepository)
        {
            _mapper = mapper;
            _draftRepository = draftRepository;
        }

        public async Task<DraftDetailDto> Handle(GetDraftDetailQuery request, CancellationToken cancellationToken)
        {
            var draft = await _draftRepository.GetDraftByIdWithDetails(request.DraftId);

            if (draft == null)
            {
                throw new NotFoundException(nameof(Draft), request.DraftId);
            }

            var draftDetailDto = _mapper.Map<DraftDetailDto>(draft);

            return draftDetailDto;
        }
    }
}

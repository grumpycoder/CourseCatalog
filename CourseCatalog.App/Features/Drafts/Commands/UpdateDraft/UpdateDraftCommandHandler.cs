using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Drafts.Commands.UpdateDraft
{
    public class UpdateDraftCommandHandler : IRequestHandler<UpdateDraftCommand>
    {
        private readonly IMapper _mapper;
        private readonly IDraftRepository _draftRepository;
        private readonly ICourseRepository _courseRepository;

        public UpdateDraftCommandHandler(IMapper mapper, IDraftRepository draftRepository, ICourseRepository courseRepository)
        {
            _mapper = mapper;
            _draftRepository = draftRepository;
            _courseRepository = courseRepository;
        }

        public async Task<Unit> Handle(UpdateDraftCommand request, CancellationToken cancellationToken)
        {
            var draftToUpdate = await _draftRepository.GetDraftByIdWithDetails(request.DraftId);

            if (draftToUpdate == null) throw new NotFoundException(nameof(Draft), request.DraftId);

            //TODO: Check to add missing and remove existing delivery types to draft
            draftToUpdate.DeliveryTypes.Clear();
            foreach (var dt in request.DeliveryTypes)
            {
                draftToUpdate.DeliveryTypes.Add(new DraftDeliveryType() { DeliveryTypeId = dt.DeliveryTypeId, DraftId = request.DraftId });
            }

            _mapper.Map(request, draftToUpdate, typeof(UpdateDraftCommand), typeof(Draft));

            await _draftRepository.UpdateAsync(draftToUpdate);

            return Unit.Value;
        }
    }
}

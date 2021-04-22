﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;
using WebGrease.Css.Extensions;

namespace CourseCatalog.App.Features.Drafts.Commands.UpdateDraft
{
    public class UpdateDraftCommandHandler : IRequestHandler<UpdateDraftCommand>
    {
        private readonly IDraftRepository _draftRepository;
        private readonly IMapper _mapper;

        public UpdateDraftCommandHandler(IMapper mapper, IDraftRepository draftRepository)
        {
            _mapper = mapper;
            _draftRepository = draftRepository;
        }

        public async Task<Unit> Handle(UpdateDraftCommand request, CancellationToken cancellationToken)
        {
            var draftToUpdate = await _draftRepository.GetDraftByIdWithDetails(request.DraftId);

            if (draftToUpdate == null) throw new NotFoundException(nameof(Draft), request.DraftId);

            //draftToUpdate.DeliveryTypes.Clear();
            //foreach (var dt in request.DeliveryTypes)
            //{
            //    draftToUpdate.DeliveryTypes.Add(new DraftDeliveryType() { DeliveryTypeId = dt.DeliveryTypeId, DraftId = request.DraftId });
            //}

            draftToUpdate.DeliveryTypes
                .Where(ddt =>
                    request.DeliveryTypes.All(deliveryTypeDto => deliveryTypeDto.DeliveryTypeId != ddt.DeliveryTypeId))
                .ForEach(draftDeliveryType =>
                {
                    draftToUpdate.DeliveryTypes.Add(new DraftDeliveryType
                        {DeliveryTypeId = draftDeliveryType.DeliveryTypeId});
                });

            draftToUpdate.DeliveryTypes.RemoveAll(cdt =>
                !request.DeliveryTypes.Select(ddt => ddt.DeliveryTypeId).Contains(cdt.DeliveryTypeId));

            _mapper.Map(request, draftToUpdate, typeof(UpdateDraftCommand), typeof(Draft));

            await _draftRepository.UpdateAsync(draftToUpdate);

            return Unit.Value;
        }
    }
}
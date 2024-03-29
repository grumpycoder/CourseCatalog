﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Drafts.Commands.CreateDraftProgram
{
    public class CreateDraftProgramCommandHandler : IRequestHandler<CreateDraftProgramCommand, CreatedDraftProgramDto>
    {
        private readonly IDraftRepository _draftRepository;
        private readonly IMapper _mapper;

        public CreateDraftProgramCommandHandler(IMapper mapper, IDraftRepository draftRepository)
        {
            _mapper = mapper;
            _draftRepository = draftRepository;
        }

        public async Task<CreatedDraftProgramDto> Handle(CreateDraftProgramCommand request,
            CancellationToken cancellationToken)
        {
            var existingDraft = await _draftRepository.GetDraftByIdWithDetails(request.DraftId);

            if (existingDraft == null) throw new NotFoundException(nameof(Draft), request.DraftId);

            var existingProgram = existingDraft.Programs.FirstOrDefault(e => e.ProgramId == request.ProgramId);
            if (existingProgram != null) throw new BadRequestException("Draft already contains existing endorsement");

            existingDraft.Programs.Add(new ProgramDraft
                {ProgramId = request.ProgramId, BeginYear = request.BeginYear, EndYear = request.EndYear});

            await _draftRepository.UpdateAsync(existingDraft);

            existingDraft = await _draftRepository.GetDraftByIdWithDetails(request.DraftId);

            var program = existingDraft.Programs.FirstOrDefault(e => e.ProgramId == request.ProgramId);

            var dto = _mapper.Map<CreatedDraftProgramDto>(program);

            return dto;
        }
    }
}
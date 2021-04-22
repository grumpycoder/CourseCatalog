using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Refs.Subjects.Commands
{
    public class UpdateSubjectCommandHandler : IRequestHandler<UpdateSubjectCommand>
    {
        private readonly IMapper _mapper;
        private readonly ISubjectRepository _subjectRepository;

        public UpdateSubjectCommandHandler(IMapper mapper, ISubjectRepository subjectRepository)
        {
            _mapper = mapper;
            _subjectRepository = subjectRepository;
        }

        public async Task<Unit> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
        {
            var subjectToUpdate = await _subjectRepository.GetByIdAsync(request.SubjectId);
            if (subjectToUpdate == null) throw new NotFoundException(nameof(Subject), request.SubjectId);

            var subject = await _subjectRepository.GetSubjectBySubjectCode(request.SubjectCode);
            if (subject != null && subject.SubjectCode != subjectToUpdate.SubjectCode)
                throw new BadRequestException(
                    $"Duplicate Subject Code. Existing Subject already contains Subject Code {request.SubjectCode}");

            _mapper.Map(request, subjectToUpdate, typeof(UpdateSubjectCommand), typeof(Subject));

            await _subjectRepository.UpdateAsync(subjectToUpdate);

            return Unit.Value;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Refs.Subjects.Commands
{
    public class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ISubjectRepository _subjectRepository;

        public CreateSubjectCommandHandler(IMapper mapper, ISubjectRepository subjectRepository)
        {
            _mapper = mapper;
            _subjectRepository = subjectRepository;
        }

        public async Task<int> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.GetSubjectBySubjectCode(request.SubjectCode);
            if (subject != null)
            {
                throw new BadRequestException(
                    $"Duplicate Subject Code. Existing Subject already contains Subject Code {request.SubjectCode}");
            }
            
            subject = _mapper.Map<Subject>(request);

            subject = await _subjectRepository.AddAsync(subject);

            return subject.SubjectId;
        }
    }
}

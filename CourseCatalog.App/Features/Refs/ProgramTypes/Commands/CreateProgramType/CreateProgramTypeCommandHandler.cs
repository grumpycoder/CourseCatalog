using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Refs.ProgramTypes.Commands.CreateProgramType
{
    public class CreateProgramTypeCommandHandler : IRequestHandler<CreateProgramTypeCommand, int>
    {
        private readonly IProgramTypeRepository _clusterTypeRepository;
        private readonly IMapper _mapper;

        public CreateProgramTypeCommandHandler(IMapper mapper, IProgramTypeRepository clusterTypeRepository)
        {
            _mapper = mapper;
            _clusterTypeRepository = clusterTypeRepository;
        }

        public async Task<int> Handle(CreateProgramTypeCommand request, CancellationToken cancellationToken)
        {
            var clusterType = await _clusterTypeRepository.GetProgramTypeByName(request.Name);
            if (clusterType != null)
                throw new BadRequestException(
                    $"Duplicate Program Type Name. Existing Program Type already contains name {request.Name}");

            clusterType = _mapper.Map<ProgramType>(request);

            clusterType = await _clusterTypeRepository.AddAsync(clusterType);

            return clusterType.ProgramTypeId;
        }
    }
}
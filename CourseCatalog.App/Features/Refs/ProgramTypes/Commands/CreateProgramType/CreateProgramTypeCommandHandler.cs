using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Refs.ProgramTypes.Commands.CreateProgramType
{
    public class CreateProgramTypeCommandHandler : IRequestHandler<CreateProgramTypeCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IProgramTypeRepository _clusterTypeRepository;

        public CreateProgramTypeCommandHandler(IMapper mapper, IProgramTypeRepository clusterTypeRepository)
        {
            _mapper = mapper;
            _clusterTypeRepository = clusterTypeRepository;
        }

        public async Task<int> Handle(CreateProgramTypeCommand request, CancellationToken cancellationToken)
        {
            var clusterType = await _clusterTypeRepository.GetProgramTypeByName(request.Name);
            if (clusterType != null)
            {
                throw new BadRequestException(
                    $"Duplicate Program Type Name. Existing Program Type already contains name {request.Name}");
            }

            clusterType = _mapper.Map<ProgramType>(request);

            clusterType = await _clusterTypeRepository.AddAsync(clusterType);

            return clusterType.ProgramTypeId;
        }
    }
}

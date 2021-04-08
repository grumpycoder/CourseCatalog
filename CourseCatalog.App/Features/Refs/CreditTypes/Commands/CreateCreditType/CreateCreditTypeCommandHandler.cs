using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Refs.CreditTypes.Commands.CreateCreditType
{
    public class CreateCreditTypeCommandHandler : IRequestHandler<CreateCreditTypeCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ITagRepository _creditTypeRepository;

        public CreateCreditTypeCommandHandler(IMapper mapper, ITagRepository creditTypeRepository)
        {
            _mapper = mapper;
            _creditTypeRepository = creditTypeRepository;
        }

        public async Task<int> Handle(CreateCreditTypeCommand request, CancellationToken cancellationToken)
        {
            var creditType = await _creditTypeRepository.GetCreditTypeByName(request.Name);
            if (creditType != null)
            {
                throw new BadRequestException(
                    $"Duplicate Credit Type Name. Existing Credit Type already contains name {request.Name}");
            }

            creditType = _mapper.Map<Tag>(request);
            creditType.GroupName = "CreditType"; 

            creditType = await _creditTypeRepository.AddAsync(creditType);

            return creditType.TagId;
        }
    }
}

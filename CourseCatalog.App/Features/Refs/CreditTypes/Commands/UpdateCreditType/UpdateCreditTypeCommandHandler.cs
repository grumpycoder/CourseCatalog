using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Refs.CreditTypes.Commands.UpdateCreditType
{
    public class UpdateCreditTypeCommandHandler : IRequestHandler<UpdateCreditTypeCommand>
    {
        private readonly IMapper _mapper;
        private readonly ITagRepository _creditTypeRepository;

        public UpdateCreditTypeCommandHandler(IMapper mapper, ITagRepository creditTypeRepository)
        {
            _mapper = mapper;
            _creditTypeRepository = creditTypeRepository;
        }

        public async Task<Unit> Handle(UpdateCreditTypeCommand request, CancellationToken cancellationToken)
        {
            var creditTypeToUpdate = await _creditTypeRepository.GetByIdAsync(request.TagId);
            if (creditTypeToUpdate == null) throw new NotFoundException(nameof(Tag), request.TagId);

            var creditType = await _creditTypeRepository.GetCreditTypeByName(request.Name);
            if (creditType != null && creditType.Name != creditTypeToUpdate.Name)
            {
                throw new BadRequestException(
                    $"Duplicate Name. Existing Credit Type already contains name {request.Name}");
            }

            _mapper.Map(request, creditTypeToUpdate, typeof(UpdateCreditTypeCommand), typeof(Tag));

            await _creditTypeRepository.UpdateAsync(creditTypeToUpdate);

            return Unit.Value;
        }
    }
}

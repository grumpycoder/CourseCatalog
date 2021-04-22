using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Refs.CreditTypes.Commands.DeleteCreditType
{
    public class DeleteCreditTypeCommandHandler : IRequestHandler<DeleteCreditTypeCommand>
    {
        private readonly ITagRepository _tagRepository;

        public DeleteCreditTypeCommandHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<Unit> Handle(DeleteCreditTypeCommand request, CancellationToken cancellationToken)
        {
            var creditTypeToDelete = await _tagRepository.GetByIdAsync(request.TagId);

            if (creditTypeToDelete == null) throw new NotFoundException(nameof(Tag), request.TagId);

            if (await _tagRepository.HasCourses(creditTypeToDelete.Name))
                throw new BadRequestException("Credit Type assigned to courses. Cannot delete.");

            return Unit.Value;
        }
    }
}
using MediatR;
using NewsApp.Application.DTOs;
using NewsApp.Domain.Entities;
using NewsApp.Domain.Interfaces;

namespace NewsApp.Application.Commands
{
    public record CreateCategoryCommand(CreateCategoryDto Data) : IRequest<Guid>;

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category { Id = Guid.NewGuid(), Name = request.Data.Name, Slug = request.Data.Slug };
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
            return category.Id;
        }
    }
}
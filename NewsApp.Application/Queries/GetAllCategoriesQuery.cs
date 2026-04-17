using MediatR;
using NewsApp.Application.DTOs;
using NewsApp.Domain.Interfaces;

namespace NewsApp.Application.Queries
{
    public record GetAllCategoriesQuery : IRequest<IEnumerable<CategoryDto>>;

    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            return categories.Select(c => new CategoryDto(c.Id, c.Name, c.Slug));
        }
    }
}
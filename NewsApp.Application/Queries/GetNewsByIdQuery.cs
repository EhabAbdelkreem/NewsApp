using MediatR;
using NewsApp.Application.DTOs;
using NewsApp.Domain.Interfaces;

namespace NewsApp.Application.Queries
{
    public record GetNewsByIdQuery(Guid Id) : IRequest<NewsDto?>;

    public class GetNewsByIdQueryHandler : IRequestHandler<GetNewsByIdQuery, NewsDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetNewsByIdQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<NewsDto?> Handle(GetNewsByIdQuery request, CancellationToken cancellationToken)
        {
            var n = await _unitOfWork.News.GetByIdAsync(request.Id);
            if (n == null) return null;

            return new NewsDto
            {
                Id = n.Id,
                Title = n.Title,
                Content = n.Content,
                ImageUrl = n.ImageUrl,
                VideoUrl = n.VideoUrl,
                PublishDate = n.PublishDate,
                IsBreaking = n.IsBreaking,
                CategoryName = n.Category?.Name ?? "بدون قسم"
            };
        }
    }
}
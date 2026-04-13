using MediatR;
using NewsApp.Application.DTOs;
using NewsApp.Domain.Interfaces;

public class GetLatestNewsQueryHandler : IRequestHandler<GetLatestNewsQuery, IEnumerable<NewsDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetLatestNewsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<NewsDto>> Handle(GetLatestNewsQuery request, CancellationToken cancellationToken)
    {
        var news = await _unitOfWork.News.GetAllAsync();

        return news.Select(n => new NewsDto
        {
            Id = n.Id,
            Title = n.Title,
            Content = n.Content,
            ImageUrl = n.ImageUrl,
            VideoUrl = n.VideoUrl,
            PublishDate = n.PublishDate,
            IsBreaking = n.IsBreaking,
            CategoryName = n.Category?.Name ?? "بدون قسم"
        });
    }
}
}IRequestHandler < GetLatest
using MediatR;
using NewsApp.Application.DTOs;
using NewsApp.Domain.Entities;
using NewsApp.Domain.Interfaces;

namespace NewsApp.Application.Commands
{
    public class CreateNewsCommand : IRequest<Guid>
    {
        public CreateNewsDto NewsData { get; set; } = new CreateNewsDto();
    }

    public class CreateNewsCommandHandler : IRequestHandler<CreateNewsCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateNewsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateNewsCommand request, CancellationToken cancellationToken)
        {
            var news = new News
            {
                Id = Guid.NewGuid(),
                Title = request.NewsData.Title,
                Content = request.NewsData.Content,
                ImageUrl = request.NewsData.ImageUrl,
                VideoUrl = request.NewsData.VideoUrl,
                IsBreaking = request.NewsData.IsBreaking,
                CategoryId = request.NewsData.CategoryId,
                PublishDate = DateTime.UtcNow,
                Views = 0
            };

            await _unitOfWork.News.AddAsync(news);
            await _unitOfWork.SaveChangesAsync();

            return news.Id;
        }
    }
}

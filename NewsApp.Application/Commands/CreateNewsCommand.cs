using MediatR;
using NewsApp.Application.DTOs;
using NewsApp.Domain.Entities;
using NewsApp.Domain.Interfaces;

namespace NewsApp.Application.Commands
{
    // تعريف الـ Command كـ Record يأخذ الـ Data
    public record CreateNewsCommand(CreateNewsDto Data) : IRequest<Guid>;

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
                Title = request.Data.Title,
                Content = request.Data.Content,
                ImageUrl = request.Data.ImageUrl,
                VideoUrl = request.Data.VideoUrl,
                IsBreaking = request.Data.IsBreaking,
                CategoryId = request.Data.CategoryId,
                PublishDate = DateTime.UtcNow,
                Views = 0
            };

            await _unitOfWork.News.AddAsync(news);
            await _unitOfWork.SaveChangesAsync();
            return news.Id;
        }
    }
}
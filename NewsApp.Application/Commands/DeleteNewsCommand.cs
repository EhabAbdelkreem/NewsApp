using MediatR;
using NewsApp.Domain.Interfaces;

namespace NewsApp.Application.Commands
{
    public record DeleteNewsCommand(Guid Id) : IRequest<bool>;

    public class DeleteNewsCommandHandler : IRequestHandler<DeleteNewsCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteNewsCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<bool> Handle(DeleteNewsCommand request, CancellationToken cancellationToken)
        {
            var news = await _unitOfWork.News.GetByIdAsync(request.Id);
            if (news == null) return false;

            _unitOfWork.News.Delete(news);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
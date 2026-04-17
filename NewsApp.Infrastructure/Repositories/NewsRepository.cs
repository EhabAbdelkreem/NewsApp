using Microsoft.EntityFrameworkCore;
using NewsApp.Domain.Entities;
using NewsApp.Domain.Interfaces;
using NewsApp.Infrastructure.Data;

namespace NewsApp.Infrastructure.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly NewsDbContext _context;
        public NewsRepository(NewsDbContext context) => _context = context;

        public async Task<IEnumerable<News>> GetAllAsync()
        {
            return await _context.News
                .Include(n => n.Category)
                .OrderByDescending(n => n.PublishDate)
                .ToListAsync();
        }

        public async Task<News?> GetByIdAsync(Guid id)
        {
            return await _context.News
                .Include(n => n.Category)
                .FirstOrDefaultAsync(n => n.Id == id);
        }

        // الدالة التي كانت مفقودة وتسببت في الخطأ
        public async Task<IEnumerable<News>> GetBreakingNewsAsync()
        {
            return await _context.News
                .Where(n => n.IsBreaking)
                .OrderByDescending(n => n.PublishDate)
                .Take(5) // جلب آخر 5 أخبار عاجلة مثلاً
                .ToListAsync();
        }

        public async Task AddAsync(News news) => await _context.News.AddAsync(news);

        public void Update(News news) => _context.News.Update(news);

        public void Delete(News news) => _context.News.Remove(news);
    }
}
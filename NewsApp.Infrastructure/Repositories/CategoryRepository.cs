using Microsoft.EntityFrameworkCore;
using NewsApp.Domain.Entities;
using NewsApp.Domain.Interfaces;
using NewsApp.Infrastructure.Data;

namespace NewsApp.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly NewsDbContext _context;
        public CategoryRepository(NewsDbContext context) => _context = context;
        public async Task<IEnumerable<Category>> GetAllAsync() => await _context.Categories.ToListAsync();
        public async Task<Category?> GetByIdAsync(Guid id) => await _context.Categories.FindAsync(id);
        public async Task AddAsync(Category category) => await _context.Categories.AddAsync(category);
    }
}
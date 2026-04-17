using NewsApp.Domain.Interfaces;
using NewsApp.Infrastructure.Data;

namespace NewsApp.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NewsDbContext _context;
        public INewsRepository News { get; }
        public ICategoryRepository Categories { get; }
        public IUserRepository Users { get; }

        public UnitOfWork(NewsDbContext context)
        {
            _context = context;
            News = new NewsRepository(_context);
            Categories = new CategoryRepository(_context);
            Users = new UserRepository(_context);
        }
        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
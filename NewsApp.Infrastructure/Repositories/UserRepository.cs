using Microsoft.EntityFrameworkCore;
using NewsApp.Domain.Entities;
using NewsApp.Domain.Interfaces;
using NewsApp.Infrastructure.Data;

namespace NewsApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NewsDbContext _context;
        public UserRepository(NewsDbContext context) { _context = context; }

        public async Task<User?> GetByEmailAsync(string email) => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        public async Task AddAsync(User user) => await _context.Users.AddAsync(user);
    }
}
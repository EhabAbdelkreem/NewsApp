using Microsoft.EntityFrameworkCore;
using NewsApp.Domain.Entities;

namespace NewsApp.Infrastructure.Data
{
    public class NewsDbContext : DbContext
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options) { }

        public DbSet<News> News { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // استخدام Guid ثابتة لتجنب رسالة (PendingModelChangesWarning)
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "سياسة",
                    Slug = "politics"
                },
                new Category
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "رياضة",
                    Slug = "sports"
                }
            );
        }
    }
}
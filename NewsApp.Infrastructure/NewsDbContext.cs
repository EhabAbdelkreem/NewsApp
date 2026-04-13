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

            // إضافة بيانات أولية (Seed Data) للأقسام لتسهيل التجربة
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = Guid.NewGuid(), Name = "سياسة", Slug = "politics" },
                new Category { Id = Guid.NewGuid(), Name = "رياضة", Slug = "sports" },
                new Category { Id = Guid.NewGuid(), Name = "اقتصاد", Slug = "economy" }
            );
        }
    }
}
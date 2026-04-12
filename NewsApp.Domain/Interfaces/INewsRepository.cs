using NewsApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsApp.Domain.Interfaces
{
    {
    public interface INewsRepository
    {
        Task<IEnumerable<News>> GetAllAsync();
        Task<News?> GetByIdAsync(Guid id);
        Task<IEnumerable<News>> GetBreakingNewsAsync();
        Task AddAsync(News news);
        void Update(News news);
        void Delete(News news);
    }
}

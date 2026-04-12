using System;
using System.Collections.Generic;
using System.Text;

namespace NewsApp.Domain.Interfaces
{

    public interface IUnitOfWork
    {
        INewsRepository News { get; }
        ICategoryRepository Categories { get; }
        IUserRepository Users { get; }
        Task<int> SaveChangesAsync();
    }
}

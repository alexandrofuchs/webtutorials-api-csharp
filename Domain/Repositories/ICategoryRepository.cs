using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebTutorialsApp.Domain.Entities;

namespace WebTutorialsApp.Domain.Repositories
{
    public interface ICategoryRepository : IDisposable
    {
        Task<IEnumerable<Category>> Get();
        Task<Category> GetBy(Guid id);
        Task<Category> GetBy(string description);   
        Task Create(Category entity);
        Task Update(Category entity);
        Task Delete(Category entity);
    }
}

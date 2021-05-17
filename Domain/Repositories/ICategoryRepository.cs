using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebTutorialsApp.Domain.Entities;

namespace WebTutorialsApp.Domain.Repositories
{
    public interface ICategoryRepository : IDisposable
    {
        Task<int> Count();
        Task<IEnumerable<Category>> Get();
        Task<IEnumerable<Category>> GetByPage(int pageIndex, int maxItemsPerPage);  
        Task<Category> GetBy(Guid id);
        Task<Category> GetBy(string description);   
        Task Create(Category entity);
        Task Update(Category entity);
        Task Delete(Category entity);
    }
}

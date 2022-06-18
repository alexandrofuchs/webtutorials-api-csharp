using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebTutorialsApp.Domain.Entities;
using WebTutorialsApp.Domain.Models;

namespace WebTutorialsApp.Domain.Services
{
    public interface ICategoryService : IDisposable
    {
        Task<int> Count();

        Task<List<Category>> Get();

        Task<Category> GetBy(Guid? id);

        Task<Category> GetBy(string description);        

        Task<IEnumerable<Category>> GetByPage(int? pageIndex, int? maxItemsPerPage);

        Task<Category> Create(CategoryModel model);

        Task<Category> Update(Category model);

        Task<Category> Delete(Category model);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebTutorialsApp.Domain.Entities;
using WebTutorialsApp.Domain.Models;

namespace WebTutorialsApp.Domain.Services
{
    public interface ICategoryService : IDisposable
    {
        Task<Category> GetBy(Guid? id);

        Task<Category> GetBy(string? description);

        Task<IEnumerable<Category>> Get();

        Task Create(CategoryModel model);

        Task Update(CategoryModel model);

        Task Delete(CategoryModel model);
    }
}

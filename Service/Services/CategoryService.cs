using WebTutorialsApp.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTutorialsApp.Domain.Entities;
using WebTutorialsApp.Domain.Models;
using WebTutorialsApp.Domain.Repositories;
using WebTutorialsApp.Domain.Services;

namespace WebTutorialsApp.Middleware.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository) => _categoryRepository = categoryRepository;

        public async Task<IEnumerable<Category>> GetByPage(int? pageIndex = 0, int? maxItemsPerPage = 10)
        {
            var categories = await _categoryRepository.GetByPage(pageIndex.Value, maxItemsPerPage.Value);
            return categories.OrderBy(c => c.Description);
        }

        public async Task<int> Count()
        {
            return await _categoryRepository.Count();
        }

        public async Task<List<Category>> Get()
        {
            IEnumerable<Category> categories;
            categories = await _categoryRepository.Get();
            return categories.ToList();
        }

        public async Task<Category> GetBy(Guid? id)
        {
            if (!id.HasValue)
            {
                throw new Exception("Invalid Category!");
            }
            var foundCategory = await _categoryRepository.GetBy(id.Value);
            if (foundCategory == null)
            {
                throw new Exception("Invalid Category!");
            }
            return foundCategory;
        }

         public Task<Category> GetBy(string description)
        {
            throw new NotImplementedException();
        }

        public async Task<Category> Create(CategoryModel model)
        {
            if (!model.IsModelValid())
            {
                throw new InvalidModelException(model.Notifications);
            }
            if (await _categoryRepository.GetBy(model.Description.Value) != null)
            {
                throw new Exception("category description already exists!");
            }
            return await  _categoryRepository.Create(model.ToEntity());
        }

        public async Task<Category> Update(Category model)
        {
            return await _categoryRepository.Update(model);
        }

        public async Task<Category> Delete(Category model)
        {
           return await _categoryRepository.Delete(model);
        }

        public void Dispose() => _categoryRepository?.Dispose();

    }
}

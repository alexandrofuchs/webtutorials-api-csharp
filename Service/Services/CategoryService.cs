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

        public async Task<IEnumerable<Category>> Get()
        {
            var categories = await _categoryRepository.Get();
            return categories.OrderBy(c => c.Description);
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

         public Task<Category> GetBy(string? description)
        {
            throw new NotImplementedException();
        }

        public async Task Create(CategoryModel model)
        {
            if (!model.IsModelValid())
            {
                throw new InvalidModelException(model.Notifications);
            }
            if (await _categoryRepository.GetBy(model.Description.Value) != null)
            {
                throw new Exception("category description already exists!");
            }
            await _categoryRepository.Create(model.ToEntity());
        }

        public Task Update(CategoryModel model)
        {
            throw new NotImplementedException();
        }

        public Task Delete(CategoryModel model)
        {
            throw new NotImplementedException();
        }

        public void Dispose() => _categoryRepository?.Dispose();
    }
}

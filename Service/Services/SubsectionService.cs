using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTutorialsApp.Common.Exceptions;
using WebTutorialsApp.Domain.Entities;
using WebTutorialsApp.Domain.Models;
using WebTutorialsApp.Domain.Repositories;
using WebTutorialsApp.Domain.Services;

namespace WebTutorialsApp.Middleware.Services
{
    public class SubsectionService : ISubsectionService
    {
        private readonly ISubsectionRepository _subsectionRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPostRepository _moduleRepository;

        public SubsectionService(ISubsectionRepository subsectionRepository)
        {
            _subsectionRepository = subsectionRepository;
        }

        public SubsectionService(ISubsectionRepository subsectionRepository, ICategoryRepository categoryRepository)
        {
            _subsectionRepository = subsectionRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<int> GetPostsCount()
        {
            return await _subsectionRepository.Count();
        }

        public async Task<int> GetPostsCount(Guid? categoryId = null)
        {
            if (!categoryId.HasValue)
            {
                throw new Exception("Invalid Category!");
            }
            return await _subsectionRepository.CountBy(categoryId.Value);
        }

        public async Task<int> GetPostPagesCount(Guid? categoryId = null, int? maxPageItems = 5)
        {
            var totalCourses = await GetPostsCount(categoryId.Value);
            if (!maxPageItems.HasValue)
            {
                throw new Exception("Max page items value is not valid!");
            }
            double totalPages = totalCourses / maxPageItems.Value;
            return (int)Math.Ceiling(totalPages);
        }

        public async Task<IEnumerable<Subsection>> GetByCategory(Guid? categoryId = null)
        {
            if (!categoryId.HasValue)
            {
                throw new Exception("Invalid Category Id!");
            }
            var courseEntities = await _subsectionRepository.GetByCategory(categoryId.Value);

            return courseEntities.ToList().OrderBy(c => c.Description);
        }

        //public async Task<IEnumerable<Subsection>> GetByCategory(Guid? categoryId = null, int? pageIndex = 0, int? maxPageItems = 5)
        //{
        //    if (categoryId == null)
        //    {
        //        throw new Exception("Invalid Category!");
        //    }
        //    var courseEntities = await _subsectionRepository.GetByCategory(categoryId.Value, pageIndex, maxPageItems);

        //    return courseEntities.ToList().OrderBy(c => c.Description);
        //}

        public async Task Create(SubsectionModel model)
        {
            if (!model.IsModelValid())
            {
                throw new InvalidModelException(model.Notifications);
            }
            if (await _categoryRepository.GetBy(model.CategoryId) == null)
            {
                throw new Exception("Invalid Category!");
            }
            if (await _subsectionRepository.GetBy(model.Description.Value) != null)
            {
                throw new Exception("Subsection description already Exists!");
            }
            await _subsectionRepository.Create(model.ToEntity());
        }

        public void Dispose()
        {
            _subsectionRepository?.Dispose();
            _categoryRepository?.Dispose();
        }

        public async Task<Subsection> GetBy(Guid id) => throw new NotImplementedException();

        public IEnumerable<Post> GetCourseModules(Guid categoryId) => throw new NotImplementedException();
    }
}

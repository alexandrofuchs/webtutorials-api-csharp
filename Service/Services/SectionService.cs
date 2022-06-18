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
    public class SectionService : ISectionService
    {
        private readonly ISectionRepository _subsectionRepository;
        private readonly ICategoryRepository _categoryRepository;

        public SectionService(ISectionRepository subsectionRepository)
        {
            _subsectionRepository = subsectionRepository;
        }

        public SectionService(ISectionRepository subsectionRepository, ICategoryRepository categoryRepository)
        {
            _subsectionRepository = subsectionRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<int> Count()
        {
            return await _subsectionRepository.Count();
        }

        public async Task<IEnumerable<Section>> GetByCategory(Guid? categoryId = null, int? pageIndex = 0, int? maxPageItems = 10)
        {
            if (!categoryId.HasValue)
            {
                throw new Exception("invalid category id");
            }
            return await _subsectionRepository.GetByCategory(categoryId.Value, pageIndex, maxPageItems);
        }

        public async Task<int> GetPostsCount()
        {
            return await _subsectionRepository.Count();
        }

        public async Task<IEnumerable<Section>> GetSections(Guid? categoryId)
        {
            if (!categoryId.HasValue)
            {
                throw new Exception("invalid category Id");
            }
            return await _subsectionRepository.GetSections(categoryId.Value);
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

        public async Task<Section> Create(SectionModel model)
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
            return await _subsectionRepository.Create(model.ToEntity());
        }

        public void Dispose()
        {
            _subsectionRepository?.Dispose();
            _categoryRepository?.Dispose();
        }

        public async Task<Section> GetBy(Guid? id){
            if (!id.HasValue)
            {
                throw new Exception("Invalid Id");
            }
            return await _subsectionRepository.GetBy(id.Value);
        }

        public async Task<Section> Delete(Section model)
        {
            var countVideos = await _subsectionRepository.GetVideosCount(model.Id);
            if(countVideos > 0)
            {
                throw new Exception("Invalid Operation");
            }
            return await _subsectionRepository.Delete(model);
        }
    }
}

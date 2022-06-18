using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebTutorialsApp.Domain.Entities;
using WebTutorialsApp.Domain.Models;

namespace WebTutorialsApp.Domain.Services
{
    public interface ISectionService : IDisposable
    {
        Task<int> Count();

        Task<Section> GetBy(Guid? id);    

        Task<int> GetPostsCount(Guid? sectionId = null);

        Task<int> GetPostPagesCount(Guid? sectionId = null, int? maxItemsPage = 5);

        Task<Section> Create(SectionModel model);

        Task<Section> Delete(Section model);

        Task<IEnumerable<Section>> GetByCategory(Guid? categoryId=null, int? pageIndex=0, int? maxPageItems=10);

        Task<IEnumerable<Section>> GetSections(Guid? categoryId = null);
    }
}

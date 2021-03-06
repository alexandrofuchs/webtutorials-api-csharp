using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebTutorialsApp.Domain.Entities;

namespace WebTutorialsApp.Domain.Repositories
{
    public interface ISectionRepository : IDisposable
    {
        Task<Section> GetBy(Guid id);
        Task<Section> GetBy(string description);
        Task<IEnumerable<Section>> GetByCategory(Guid categoryId, int? index = 0, int? maxItems = 5);
        Task<IEnumerable<Section>> GetSections(Guid categoryId);
        Task<int> Count();
        Task<int> CountBy(Guid sectionId);
        Task<int> GetVideosCount(Guid sectionId);
        Task<Section> Create(Section entity);
        Task<Section> Update(Section entity);
        Task<Section> Delete(Section entity);
    }
}

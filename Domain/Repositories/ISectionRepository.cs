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
        Task Create(Section entity);
        Task Update(Section entity);
        Task Delete(Section entity);
    }
}

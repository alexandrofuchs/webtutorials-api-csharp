using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebTutorialsApp.Domain.Entities;

namespace WebTutorialsApp.Domain.Repositories
{
    public interface ISubsectionRepository : IDisposable
    {
        Task<Subsection> GetBy(Guid id);
        Task<Subsection> GetBy(string description);
        Task<IEnumerable<Subsection>> GetByCategory(Guid categoryId, int? index = 0, int? maxItems = 5);
        Task<int> Count();
        Task<int> CountBy(Guid subsectionId);        
        Task Create(Subsection entity);
        Task Update(Subsection entity);
        Task Delete(Subsection entity);
    }
}

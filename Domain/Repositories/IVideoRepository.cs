using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebTutorialsApp.Domain.Entities;

namespace WebTutorialsApp.Domain.Repositories
{
    public interface IVideoRepository : IDisposable
    {
        Task<IEnumerable<Video>> Get();
        Task<IEnumerable<Video>> Get(int? pageIndex, int? maxItemsPerPage);
        Task<IEnumerable<Video>> GetVideos();
        Task<Video> GetBy(Guid id);
        Task<Video> GetBy(string description);
        Task<IEnumerable<Video>> GetBySection(Guid sectionId);
        Task<IEnumerable<Video>> GetByCategory(Guid categoryId);
        Task Create(Video entity);
        Task Update(Video entity);
        Task Delete(Video entity);
    }
}

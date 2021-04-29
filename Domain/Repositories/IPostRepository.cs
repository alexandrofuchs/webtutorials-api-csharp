using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebTutorialsApp.Domain.Entities;

namespace WebTutorialsApp.Domain.Repositories
{
    public interface IPostRepository : IDisposable
    {
        Task<Post> GetBy(Guid id);
        Task<Post> GetBy(string description);
        Task<IEnumerable<Post>> GetBySection(Guid subsectionId);
        Task Create(Post entity);
        Task Update(Post entity);
        Task Delete(Post entity);
    }
}

using System;
using System.Threading.Tasks;
using WebTutorialsApp.Domain.Entities;

namespace WebTutorialsApp.Domain.Repositories
{
    public interface IFileRepository : IDisposable
    {
        Task Create(PostFile entity);
        Task Update(PostFile entity);
        Task Remove(PostFile entity);
        Task<PostFile> GetBy(Guid id);     
    }
}

using System;
using System.Threading.Tasks;
using WebTutorialsApp.Domain.Entities;

namespace WebTutorialsApp.Domain.Services
{
    public interface IFileService : IDisposable
    {
        Task Create(PostFile model);
        Task Update(PostFile model);
        Task Remove(PostFile model);
        Task<PostFile> GetVideoClass(Guid? id);
    }
}

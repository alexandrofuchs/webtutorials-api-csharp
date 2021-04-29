using System;
using System.Threading.Tasks;
using WebTutorialsApp.Domain.Entities;
using WebTutorialsApp.Domain.Repositories;
using WebTutorialsApp.Domain.Services;

namespace WebTutorialsApp.Middleware.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;

        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<PostFile> GetVideoClass(Guid? id)
        {
            if (!id.HasValue)
            {
                throw new Exception("Invalid id!");
            }
            var foundVideoClass = await _fileRepository.GetBy(id.Value);
            if (foundVideoClass == null)
            {
                throw new Exception("not found!");
            }
            return foundVideoClass;
        }

        public async Task Create(PostFile model)
        {     
            await _fileRepository.Create(model);
        }

        public Task Update(PostFile model)
        {
            throw new NotImplementedException();
        }

        public Task Remove(PostFile model)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _fileRepository.Dispose();
        }
    }
}

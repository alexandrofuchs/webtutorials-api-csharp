using System;
using System.Threading.Tasks;
using WebTutorialsApp.Domain.Entities;
using WebTutorialsApp.Domain.Repositories;
using WebTutorialsApp.Persistence.Data;

namespace WebTutorialsApp.Persistence.Repositories
{
    public class FileRepository : Repository<PostFile>, IFileRepository
    {
        public FileRepository(WebTutorialsAppDbContext context) : base(context) { }

        public async Task<PostFile> GetBy(Guid id) 
            => await GetOne(v => v.Id.Equals(id));

        public async Task Create(PostFile entity) 
            => await CreateOne(entity);

        public async Task Remove(PostFile entity) 
            => await DeleteOne(entity);

        public async Task Update(PostFile entity) 
            => await UpdateOne(entity);
    }
}

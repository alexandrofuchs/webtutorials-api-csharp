using System;
using System.Collections.Generic;
using System.Linq;
using WebTutorialsApp.Domain.Repositories;
using WebTutorialsApp.Persistence.Data;
using WebTutorialsApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace WebTutorialsApp.Persistence.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(WebTutorialsAppDbContext context) : base(context) { }

        public async Task<Post> GetBy(Guid id)
            => await GetOne(x => x.Id.Equals(id));

        public async Task<Post> GetBy(string description)
            => await GetOne(x => x.Description.Trim().ToLower().Equals(description.Trim().ToLower()));

        public async Task<IEnumerable<Post>> GetBySection(Guid subsectionId)
            => await DbSet.Where(m => m.SubsectionId.Equals(subsectionId))
                //.Include(m => m.File)
                .ToListAsync();

        public async Task Create(Post entity)
            => await CreateOne(entity);

        public async Task Update(Post entity)
            => await UpdateOne(entity);

        public async Task Delete(Post entity)
            => await DeleteOne(entity);
    }
}

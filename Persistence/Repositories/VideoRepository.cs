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
    public class VideoRepository : Repository<Video>, IVideoRepository
    {
        public VideoRepository(WebTutorialsAppDbContext context) : base(context) { }

        public async Task<Video> GetBy(Guid id)
            => await GetOne(x => x.Id.Equals(id));

        public async Task<Video> GetBy(string description)
            => await GetOne(x => x.FileName.Trim().ToLower().Equals(description.Trim().ToLower()));

        public async Task<IEnumerable<Video>> GetBySection(Guid subsectionId)
            => await DbSet.Where(m => m.SectionId.Equals(subsectionId))
                .ToListAsync();

        public async Task<IEnumerable<Video>> GetByCategory(Guid categoryId)
           => await DbSet.Where(x => x.Section.CategoryId.Equals(categoryId))
               .ToListAsync();

        public async Task<IEnumerable<Video>> GetVideos()
        {
           return await GetAllOrdernedByCreation( v => v.UpdatedAt);
        }

        public async Task Create(Video entity)
            => await CreateOne(entity);

        public async Task Update(Video entity)
            => await UpdateOne(entity);

        public async Task Delete(Video entity)
            => await DeleteOne(entity);

        public Task<IEnumerable<Video>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Video>> Get(int? pageIndex, int? maxItemsPerPage)
        {
            throw new NotImplementedException();
        }
    }
}

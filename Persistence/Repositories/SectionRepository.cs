using System;
using System.Collections.Generic;
using WebTutorialsApp.Domain.Repositories;
using WebTutorialsApp.Persistence.Data;
using WebTutorialsApp.Domain.Entities;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebTutorialsApp.Persistence.Repositories
{
    public class SectionRepository : Repository<Section>, ISectionRepository
    {
        public SectionRepository(WebTutorialsAppDbContext context) : base(context) { }

        public async Task<Section> GetBy(Guid id) => await DbSet.Where(c => c.Id.Equals(id)).Include( x => x.Videos).FirstOrDefaultAsync();

        public async Task<Section> GetBy(string description) => await GetOne(c => c.Description.Trim().ToLower().Equals(description.Trim().ToLower()));

        public async Task<int> Count() => await GetCount();

        public async Task<int> CountBy(Guid categoryId) => await GetCountBy(c => c.CategoryId.Equals(categoryId));   

        public async Task<IEnumerable<Section>> GetByCategory(Guid categoryId, int? index = 0, int? maxItems = 5)
            => await GetItemsPaginated(c => c.Description, index.Value, maxItems.Value);        

        public async Task<Section> Create(Section entity) => await CreateOne(entity);

        public async Task<Section> Update(Section entity) => await UpdateOne(entity);

        public async Task<Section> Delete(Section entity) => await DeleteOne(entity);

        public async Task<IEnumerable<Section>> GetSections(Guid categoryId)
        {
            return await DbSet.Where(x => x.CategoryId.Equals(categoryId)).ToListAsync();
        }

        public async Task<int> GetVideosCount(Guid sectionId)
        {
           var section = await DbSet.Where(x => x.Id.Equals(sectionId)).FirstOrDefaultAsync();
            return section.Videos.Count; 
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTutorialsApp.Domain.Entities;
using WebTutorialsApp.Domain.Repositories;
using WebTutorialsApp.Persistence.Data;

namespace WebTutorialsApp.Persistence.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(WebTutorialsAppDbContext context) : base(context) { }

        public async Task<Category> GetBy(Guid id) =>
            await DbSet.Where(c => c.Id.Equals(id))
            .Include(c => c.Sections.OrderByDescending( x=> x.Description))            
            .FirstOrDefaultAsync();

        public async Task<int> Count() => await GetCount();

        public async Task<Category> GetBy(string description) => await GetOne(c => c.Description.Trim().ToLower().Equals(description.Trim().ToLower()));

        public async Task<IEnumerable<Category>> Get() => await DbSet.ToListAsync();

        public async Task<IEnumerable<Category>> GetByPage(int pageIndex, int maxItemsPerPage) 
            => await GetItemsPaginated(x => x.Description, pageIndex, maxItemsPerPage);

        public async Task Create(Category entity) => await CreateOne(entity);

        public async Task Update(Category entity) => await UpdateOne(entity);

        public async Task Delete(Category entity) => await DeleteOne(entity);

  
    }
}

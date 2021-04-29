using System;
using System.Collections.Generic;
using WebTutorialsApp.Domain.Repositories;
using WebTutorialsApp.Persistence.Data;
using WebTutorialsApp.Domain.Entities;
using System.Threading.Tasks;

namespace WebTutorialsApp.Persistence.Repositories
{
    public class SubsectionRepository : Repository<Subsection>, ISubsectionRepository
    {
        public SubsectionRepository(WebTutorialsAppDbContext context) : base(context) { }

        public async Task<Subsection> GetBy(Guid id) => await GetOne(c => c.Id.Equals(id));

        public async Task<Subsection> GetBy(string description) => await GetOne(c => c.Description.Trim().ToLower().Equals(description.Trim().ToLower()));

        public async Task<int> Count() => await GetCount();

        public async Task<int> CountBy(Guid categoryId) => await GetCountBy(c => c.CategoryId.Equals(categoryId));   

        public async Task<IEnumerable<Subsection>> GetByCategory(Guid categoryId, int? index = 0, int? maxItems = 5)
            => await GetMany(c => c.CategoryId.Equals(categoryId), c => c.CreatedAt, index.Value, maxItems.Value);        

        public async Task Create(Subsection entity) => await CreateOne(entity);

        public async Task Update(Subsection entity) => await UpdateOne(entity);

        public async Task Delete(Subsection entity) => await DeleteOne(entity);
    }
}

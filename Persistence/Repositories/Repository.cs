using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebTutorialsApp.Persistence.Data;

namespace WebTutorialsApp.Persistence.Repositories
{
    public abstract class Repository<Entity>
        where Entity : class
    {
        private readonly WebTutorialsAppDbContext _context;
        protected DbSet<Entity> DbSet { get; }

        public Repository(WebTutorialsAppDbContext context)
        {
            _context = context;
            DbSet = _context.Set<Entity>();
        }

        protected virtual async Task<IEnumerable<Entity>> GetAll() => await DbSet.ToListAsync();

        public async Task<int> GetCount() => await DbSet.CountAsync();

        public async Task<int> GetCountBy(Expression<Func<Entity, bool>> predicate) => await DbSet.Where(predicate).CountAsync();

        protected virtual async Task<Entity> GetOne(Expression<Func<Entity, bool>> predicate)
          => await DbSet
                .AsNoTracking()
                .Where(predicate)
                .FirstOrDefaultAsync();


        protected virtual async Task<IEnumerable<Entity>> GetItemsPaginated(Expression<Func<Entity, string>> orderBy, int pageIndex, int maxItemsPerPage)
            => await DbSet
                 .OrderBy(orderBy)
                 .Skip(pageIndex * maxItemsPerPage)
                 .Take(maxItemsPerPage)
                 .ToListAsync();

        protected virtual async Task<IEnumerable<Entity>> GetManyWhere(Expression<Func<Entity, bool>> where, Expression<Func<Entity, string>> orderBy, int? index = 0, int? maxItems = 5)
             => await DbSet
                    .Where(where)
                    .OrderBy(orderBy)
                    .Skip(index.Value * maxItems.Value)
                    .Take(maxItems.Value)
                    .ToListAsync();

        protected virtual async Task<IEnumerable<Entity>> GetAllOrdernedByCreation(Expression<Func<Entity, string>> orderBy, int? index = 0, int? maxItems = 5)
             => await DbSet
                    .OrderBy(orderBy)
                    .ToListAsync();

        protected virtual async Task<Entity> CreateOne(Entity entity)
        {
            await DbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        protected virtual async Task<Entity> UpdateOne(Entity entity)
        {
            _context.Entry<Entity>(entity)
                .State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        protected virtual async Task<Entity> DeleteOne(Entity entity)
        {
            _context.Set<Entity>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public void Dispose() => _context.DisposeAsync();
    }
}

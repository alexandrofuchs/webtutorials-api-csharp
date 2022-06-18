using WebTutorialsApp.Domain.Entities;
using WebTutorialsApp.Domain.Repositories;
using WebTutorialsApp.Persistence.Data;
using System;
using System.Threading.Tasks;

namespace WebTutorialsApp.Persistence.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(WebTutorialsAppDbContext context) : base(context) { }

        public async Task<User> GetBy(Guid id) 
            => await GetOne(x => x.Id.Equals(id));   

        public async Task<User> GetBy(string email) 
            => await GetOne(x => x.Email.Equals(email));

        public async Task<User> Create(User entity)
            => await CreateOne(entity);         
        
        public async Task<User> Update(User entity)
            => await UpdateOne(entity);
        
        public async Task<User> Delete(User entity) 
            => await DeleteOne(entity);
    }
}

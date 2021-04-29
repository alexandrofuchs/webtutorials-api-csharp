using System;
using System.Threading.Tasks;
using WebTutorialsApp.Domain.Entities;

namespace WebTutorialsApp.Domain.Repositories
{
    public interface IUserRepository : IDisposable
    {
        Task<User> GetBy(Guid id);
        Task<User> GetBy(string email);
        Task Create(User entity);
        Task Update(User entity);
        Task Delete(User entity);
    }
}

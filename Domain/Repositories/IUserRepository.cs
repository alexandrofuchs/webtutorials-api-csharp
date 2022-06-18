using System;
using System.Threading.Tasks;
using WebTutorialsApp.Domain.Entities;

namespace WebTutorialsApp.Domain.Repositories
{
    public interface IUserRepository : IDisposable
    {
        Task<User> GetBy(Guid id);
        Task<User> GetBy(string email);
        Task<User> Create(User entity);
        Task<User> Update(User entity);
        Task<User> Delete(User entity);
    }
}

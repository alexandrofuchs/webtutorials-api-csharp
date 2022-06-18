using System;
using System.Threading.Tasks;
using WebTutorialsApp.Domain.Entities;
using WebTutorialsApp.Domain.Models;

namespace WebTutorialsApp.Domain.Services
{
    public interface IUserService : IDisposable
    {
        Task<User> GetBy(Guid id);
            
        Task<User> GetBy(string email);

        Task<string> Authenticate(string email, string password);

        Task<User> Register(UserModel userModel);

        Task<User> UpdateInformations(User model);

        Task<User> UpdatePassword(Guid? id, string password);

        Task<User> Delete(User userModel);
    }
}

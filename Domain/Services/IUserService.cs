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

        Task Register(UserModel userModel);

        Task UpdateInformations(UserModel userModel);

        Task UpdatePassword(string password);

        Task Delete(UserModel userModel);
    }
}

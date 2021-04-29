using WebTutorialsApp.Common.Exceptions;
using System;
using System.Threading.Tasks;
using WebTutorialsApp.Domain.Entities;
using WebTutorialsApp.Domain.Models;
using WebTutorialsApp.Domain.Repositories;
using WebTutorialsApp.Domain.Services;
using WebTutorialsApp.Middleware.Features;

namespace WebTutorialsApp.Middleware.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly PasswordEncryptation _passwordEncryptation;
        public UserService(IUserRepository repository, PasswordEncryptation passwordEncryptation)
        {
            _repository = repository;
            _passwordEncryptation = passwordEncryptation;
        }

        public async Task<User> GetBy(Guid id) => throw new NotImplementedException();

        public async Task<User> GetBy(string email) => throw new NotImplementedException();

        public async Task<string> Authenticate(string email, string password)
        {
            var foundUser = await _repository.GetBy(email);
            if (foundUser == null)
            {
                throw new Exception("Invalid Credentials!");
            }
            if (!_passwordEncryptation.Verify(password, foundUser.Password))
            {
                throw new Exception("Invalid Credentials!");
            }
            var token = TokenGenerator.GenerateToken(foundUser);
            return token;
        }

        public async Task Register(UserModel model)
        {
            if (!model.IsModelValid())
            {
                throw new InvalidModelException(model.Notifications);
            }
            var foundUser = await _repository.GetBy(model.Email.Value);
            if (foundUser != null)
            {
                throw new Exception("Email already registered!");
            }
            model.Password.OnEncrypt(_passwordEncryptation.Encrypt(model.Password.Value));
            await _repository.Create(model.ToEntity());
        }

        public async Task Delete(UserModel userModel) => throw new NotImplementedException();

        public async Task UpdateInformations(UserModel userModel) => throw new NotImplementedException();

        public async Task UpdatePassword(string password) => throw new NotImplementedException();

        public void Dispose() => _repository?.Dispose();
    }
}

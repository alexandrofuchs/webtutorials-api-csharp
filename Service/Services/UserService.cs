using WebTutorialsApp.Common.Exceptions;
using System;
using System.Threading.Tasks;
using WebTutorialsApp.Domain.Entities;
using WebTutorialsApp.Domain.Models;
using WebTutorialsApp.Domain.Repositories;
using WebTutorialsApp.Domain.Services;
using WebTutorialsApp.Middleware.Security;
using WebTutorialsApp.Domain.ValueObjects;

namespace WebTutorialsApp.Middleware.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly PasswordEncryptation _passwordEncryptation;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public UserService(IUserRepository repository, PasswordEncryptation passwordEncryptation) : this(repository)
        {          
            _passwordEncryptation = passwordEncryptation;
        }

        public async Task<User> GetBy(Guid id) => await _repository.GetBy(id);

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

        public async Task<User> Register(UserModel model)
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
            return await _repository.Create(model.ToEntity());
        }

        public async Task<User> Delete(User userModel) => await _repository.Delete(userModel);

        public async Task<User> UpdateInformations(User user) {

            var name = new Name(user.FirstName, user.LastName);
            var email = new Email(user.Email);
            if (name.Notifications.Count > 0)
            {
                throw new  InvalidModelException(name.Notifications);
            }
            if(email.Notifications.Count > 0)
            {
                throw new InvalidModelException(email.Notifications);
            }

            var foundUser = await _repository.GetBy(email.Value);
            if (foundUser != null)
            {
                if (!foundUser.Equals(user))
                {
                    throw new Exception("Email already registered!");
                }
                
            }       
            return await  _repository.Update(user);          
        } 

        public async Task<User> UpdatePassword(Guid? id, string password) {

            if (!id.HasValue)
            {
                throw new Exception("invalid Id");
            }

            var newPassword = new Password(password);

            if(newPassword.Notifications.Count > 0)
            {
                throw new InvalidModelException(newPassword.Notifications);
            }
   
            var foundUser = await _repository.GetBy(id.Value);
            if (foundUser == null)
            {
                throw new Exception("user not found!");
            }
            foundUser.Password = _passwordEncryptation.Encrypt(password);
            return await  _repository.Update(foundUser);

        }

        public void Dispose() => _repository?.Dispose();
    }
}

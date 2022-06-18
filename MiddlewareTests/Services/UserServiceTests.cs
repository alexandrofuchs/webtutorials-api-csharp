using NUnit.Framework;
using WebTutorialsApp.Domain.Repositories;
using Moq;
using WebTutorialsApp.Domain.Entities;
using System;
using WebTutorialsApp.Domain.Services;
using WebTutorialsApp.Middleware.Services;
using WebTutorialsApp.Domain.Models;
using WebTutorialsApp.Domain.ValueObjects;
using System.Threading.Tasks;
using WebTutorialsApp.Middleware.Security;

namespace Service.Tests
{
    [TestFixture()]
    public class UserServiceTests
    {
        Mock<IUserRepository> repository;
        UserService service;
        
        [SetUp]
        public void Init()
        {
           repository = new Mock<IUserRepository>(MockBehavior.Strict);
           service = new UserService(repository.Object,new PasswordEncryptation());
        }

        [TestCase(true)]
        public async Task RegisterAsync(bool expectedResult) 
        {
            User notFoundUser = null;

            UserModel createdUser = new UserModel(
                new Name("Usuario", "Teste"),
                new Email("usuario@mail.com"),
                new Password("abc12345678")
             );

            repository.Setup(p => p.GetBy(It.IsAny<string>())).ReturnsAsync(notFoundUser);
            repository.Setup(p => p.Create(It.IsAny<User>())).ReturnsAsync(createdUser.ToEntity());
                        
            var res =  await service.Register(createdUser);

            Assert.IsInstanceOf<User>(res);
            Assert.IsNotNull(res);
        }
    }
}
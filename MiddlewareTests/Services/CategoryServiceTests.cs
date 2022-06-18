using NUnit.Framework;
using WebTutorialsApp.Middleware.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using WebTutorialsApp.Domain.Repositories;
using WebTutorialsApp.Domain.Models;
using WebTutorialsApp.Domain.Entities;
using WebTutorialsApp.Domain.ValueObjects;

namespace Categories.Tests
{

    [TestFixture()]
    public class CategoryServiceTests
    {
        Mock<ICategoryRepository> repository;
        CategoryService service;

        [SetUp]
        public void Init()
        {
            repository = new Mock<ICategoryRepository>(MockBehavior.Strict);
            service = new CategoryService(repository.Object);
        }

        [Test()]
        public async Task CreateCategoryTest()
        {
            Category cat = null;
            repository.Setup(r => r.GetBy(It.IsAny<string>())).ReturnsAsync(cat);
            repository.Setup(r => r.Create(It.IsAny<Category>())).ReturnsAsync(
                new Category(
                    new Guid(),
                    DateTime.Now.ToShortDateString(),
                    DateTime.Now.ToShortDateString(),
                    "Categoria Teste"));

            var result = await service.Create(new CategoryModel(new Description("Categoria Teste")));

            Assert.IsInstanceOf<Category>(result, "Criação de categoria falhou");
        }

        [Test()]
        public async Task FailCreateCategoryTest()
        {
            Category cat = new Category(
                    new Guid(),
                    DateTime.Now.ToShortDateString(),
                    DateTime.Now.ToShortDateString(),
                    "Categoria Teste");

            repository.Setup(r => r.GetBy(It.IsAny<string>())).ReturnsAsync(cat);
            repository.Setup(r => r.Create(It.IsAny<Category>())).ReturnsAsync(cat);

            var result = await service.Create(new CategoryModel(new Description("Categoria Teste")));

            Assert.IsInstanceOf<Exception>(result);
        }

        [Test()]
        public async Task GetACategoryTest() 
        { 

            Guid id = new Guid();

            Category cat = new Category(
                   id,
                   DateTime.Now.ToShortDateString(),
                   DateTime.Now.ToShortDateString(),
                   "Categoria Teste");

            repository.Setup(r => r.GetBy(It.IsAny<Guid>())).ReturnsAsync(cat);

            var result = await service.GetBy(id);

            Assert.IsInstanceOf<Category>(result, "Buscar uma categoria");
        }
    }
}
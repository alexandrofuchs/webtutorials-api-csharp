using NUnit.Framework;
using WebTutorialsApp.Middleware.Services;
using System;
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
        public void FailCreateCategoryTest()
        {
            Category cat = new Category(
                    new Guid(),
                    DateTime.Now.ToShortDateString(),
                    DateTime.Now.ToShortDateString(),
                    "Categoria Teste");

            repository.Setup(r => r.GetBy(It.IsAny<string>())).ReturnsAsync(cat);
            repository.Setup(r => r.Create(It.IsAny<Category>())).ReturnsAsync(cat);

            Exception ex = Assert.ThrowsAsync<Exception>(async () => await service.Create(new CategoryModel(new Description("Categoria Teste"))));
            Assert.That(ex.Message, Is.EqualTo("category description already exists!"));
        }

        [Test()]
        public async Task GetByIdTest()
        {

            Guid id = new Guid();

            Category cat = new Category(
                   id,
                   DateTime.Now.ToShortDateString(),
                   DateTime.Now.ToShortDateString(),
                   "Categoria Teste");

            repository.Setup(r => r.GetBy(It.IsAny<Guid>())).ReturnsAsync(cat);

            var result = await service.GetBy(id);

            Assert.IsInstanceOf<Category>(result, "Buscar uma categoria por id");
        }

        [Test]
        public async Task GetByDescriptionTest()
        {
            var description = "";

            Category cat = new Category(
                   new Guid(),
                   DateTime.Now.ToShortDateString(),
                   DateTime.Now.ToShortDateString(),
                   "Categoria Teste");

            repository.Setup(r => r.GetBy(It.IsAny<string>())).ReturnsAsync(cat);
            
            Exception ex = Assert.ThrowsAsync<Exception>(async () => await service.GetBy(description));

            Assert.That(ex.Message, Is.EqualTo("Invalid Description!"));
           
            description = "Categoria Teste";

            var result = await service.GetBy(description);

            Assert.IsInstanceOf<Category>(result, "Buscar Categoria válida");
        }
    }
}
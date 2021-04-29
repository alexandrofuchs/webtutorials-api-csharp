using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebTutorialsApp.Api.Models.Requests;
using WebTutorialsApp.Common.Exceptions;
using WebTutorialsApp.Domain.Models;
using WebTutorialsApp.Domain.Services;
using WebTutorialsApp.Domain.ValueObjects;

namespace WebTutorialsApp.Api.Controllers
{
    [Authorize]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/category")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _service.Get();
                return StatusCode(200, new { categories });
            }
            catch
            {
                return StatusCode(400, "Error");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/category/{id?}")]
        public async Task<IActionResult> GetCategories(Guid? id)
        {
            try
            {
                var category = await _service.GetBy(id);
                return StatusCode(200, new { category = category });
            }
            catch(Exception e)
            {
                return StatusCode(400, $"Error: {e.Message}: {e.InnerException}");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/category")]
        public async Task<IActionResult> Create(Guid? id, CategoryCreationModel creationModel)
        {
            try
            {
                var model = new CategoryModel(new Description(creationModel.Description));
                await _service.Create(model);
                return StatusCode(200, new { model });
            }
            catch (Exception exception)
            {
                if (exception is InvalidModelException)
                {
                    var e = exception as InvalidModelException;
                    return StatusCode(400, new { e.Notifications });
                }
                return StatusCode(400, $"{exception.Message}: {exception.InnerException}");
            }
        }

    }
}

using WebTutorialsApp.Api.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebTutorialsApp.Api.Models;
using WebTutorialsApp.Common.Exceptions;
using WebTutorialsApp.Domain.Models;
using WebTutorialsApp.Domain.Services;
using WebTutorialsApp.Domain.ValueObjects;
using System.Collections.Generic;

namespace WebTutorialsApp.Api.Controllers
{
    [Authorize("admin")]
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
        [Route("/categories")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _service.Get();
                return StatusCode(200,
                    new ResponseModel()
                    {
                        Data = categories,
                        TotalItems = categories.Count,
                        MaxPageItems = categories.Count                        
                    });
            }
            catch
            {
                return StatusCode(400, "Error");
            }
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("/categories/{pageIndex?}/{maxPageItems?}")]
        public async Task<IActionResult> GetCategories(int? pageIndex=0, int? maxPageItems=5)
        {
            try
            {
                var totalItems = await _service.Count();
                var categories = await _service.GetByPage(pageIndex, maxPageItems);
                var totalPages = Math.Ceiling((double)totalItems / maxPageItems.Value);
                return StatusCode(200,
                    new ResponseModel(){
                        Data = categories,
                        TotalItems = totalItems,
                        MaxPageItems = maxPageItems.Value,
                        PageIndex = pageIndex.Value,
                        TotalPages = (int) totalPages                       
                    });                    
            }
            catch
            {
                return StatusCode(400, "Error");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/category/{id?}")]
        public async Task<IActionResult> GetCategory(Guid? id)
        {
            try
            {
                var category = await _service.GetBy(id);
                return StatusCode(200, new ResponseModel()
                {
                    Data = category
                });
            }
            catch(Exception e)
            {
                return StatusCode(400, e);
            }
        }

        [HttpPost]
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


        [HttpDelete]
        [Route("/category/{id?}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            try
            {
                var category = await _service.GetBy(id);
                await _service.Delete(category);
                return StatusCode(200);
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

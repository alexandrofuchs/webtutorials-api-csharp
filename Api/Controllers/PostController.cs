using WebTutorialsApp.Api.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebTutorialsApp.Common.Exceptions;
using WebTutorialsApp.Domain.Models;
using WebTutorialsApp.Domain.Services;
using WebTutorialsApp.Domain.ValueObjects;

namespace WebTutorialsApp.Api.Controllers
{
    [Authorize]
    [ApiController]
    public class PostController : Controller
    {
        private readonly IPostService _service;

        public PostController(IPostService service)
        {
            _service = service;
        }

        //[HttpGet]
        //[AllowAnonymous]
        //[Route("/course/{courseId?}/modules")]
        //public async Task<IActionResult> GetModules(Guid? courseId)
        //{
        //    try
        //    {
        //        var modules = await _service.GetPostsWithFile(courseId);
        //        return StatusCode(200, modules);
        //    }
        //    catch (Exception error)
        //    {
        //        return StatusCode(400, $"{error.Message}: {error.InnerException}");
        //    }
        //}

        //[HttpGet]
        //[AllowAnonymous]
        //[Route("/category/{categoryId?}/courses/{pageIndex?}/{maxPageItems?}")]
        //public async Task<IActionResult> GetSubsectionsByCategory(Guid? categoryId = null, int? pageIndex = 0, int? maxPageItems = 5)
        //{
        //    try
        //    {
        //        var totalItems = 0;
        //        totalItems = await _service.GetPostsCount(categoryId);
        //        var totalPages = Math.Ceiling((double)totalItems / maxPageItems.Value);
        //        var courses = await _service.GetByCategory(categoryId, pageIndex, maxPageItems);
        //        return StatusCode(200, new GetSubsectionsResponseModel()
        //        {
        //            Data = courses,
        //            TotalItems = totalItems,
        //            TotalPageItems = courses.Count(),
        //            MaxPageItems = maxPageItems.Value,
        //            PageIndex = pageIndex.Value,
        //            TotalPages = (int)totalPages
        //        });
        //    }
        //    catch (Exception error)
        //    {
        //        return StatusCode(400, $"{error.Message} : {error.InnerException}");
        //    }
        //}

        [HttpPost]
        [AllowAnonymous]
        [Route("/subsection/{subsectionId?}/post")]
        public async Task<IActionResult> CreateModule(Guid? subsectionId, PostCreationModel creationModel)
        {
            try
            {
                var model = new PostModel(
                                new Description(creationModel.Description),
                                subsectionId.Value
                            );
                await _service.Create(model, subsectionId);
                return StatusCode(200, model);
            }
            catch (Exception exception)
            {
                if (exception is InvalidModelException)
                {
                    var e = exception as InvalidModelException;
                    return StatusCode(400, new { e.Notifications });
                }
                return StatusCode(500, $"{exception.Message}: {exception.InnerException}");
            }
        }


    }
}
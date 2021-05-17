using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebTutorialsApp.Api.Models;
using WebTutorialsApp.Api.Models.Requests;
using WebTutorialsApp.Common.Exceptions;
using WebTutorialsApp.Domain.Models;
using WebTutorialsApp.Domain.Services;
using WebTutorialsApp.Domain.ValueObjects;

namespace WebTutorialsApp.Api.Controllers
{
    [Authorize]
    [ApiController]
    public class SectionController : Controller
    {
        private readonly ISectionService _service;

        public SectionController(ISectionService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/category/{categoryId?}/sections/{pageIndex?}/{maxPageItems?}")]
        public async Task<IActionResult> GetSectionsByCategory(Guid? categoryId = null, int? pageIndex=0, int? maxPageItems=10)
        {
            try
            {
                var totalItems = await _service.Count();
                var sections = await _service.GetByCategory(categoryId, pageIndex, maxPageItems);
                var totalPages = Math.Ceiling((double)totalItems / maxPageItems.Value);
                return StatusCode(200,
                    new ResponseModel()
                    {
                        Data = sections,
                        TotalItems = totalItems,
                        MaxPageItems = maxPageItems.Value,
                        PageIndex = pageIndex.Value,
                        TotalPages = (int)totalPages
                    });
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/category/{categoryId?}/section")]
        public async Task<IActionResult> GetSubsectionsByCategory(Guid? categoryId = null)
        {
            try
            {
                var subsections = await _service.GetByCategory(categoryId);
                return StatusCode(200, subsections);
            }
            catch (Exception error)
            {
                return StatusCode(400, $"{error.Message} : {error.InnerException}");
            }
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("/category/{categoryId?}/sections")]
        public async Task<IActionResult> GetVideosByCategory(Guid? categoryId = null)
        {
            try
            {
                var sections = await _service.GetSections(categoryId);
                return StatusCode(200, new ResponseModel(){Data = sections});
            }
            catch (Exception error)
            {
                return StatusCode(400, $"{error.Message} : {error.InnerException}");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/section/{sectionId?}")]
        public async Task<IActionResult> Get(Guid? sectionId = null)
        {
            try
            {
                var section = await _service.GetBy(sectionId);
                return StatusCode(200, new ResponseModel() { Data = section });
            }
            catch (Exception error)
            {
                return StatusCode(400, $"{error.Message} : {error.InnerException}");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/category/{categoryId?}/section")]
        public async Task<IActionResult> Create(Guid? categoryId, [FromBody] SubsectionCreationModel model)
        {
            try
            {
                var createdSubsection = new SectionModel(new Description(model.Description), categoryId.Value);
                await _service.Create(createdSubsection);
                return StatusCode(200, createdSubsection );
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

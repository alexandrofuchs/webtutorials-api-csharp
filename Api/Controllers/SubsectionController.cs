using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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
    public class SubsectionController : Controller
    {
        private readonly ISubsectionService _service;

        public SubsectionController(ISubsectionService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/category/{categoryId?}/subsection")]
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

        [HttpPost]
        [AllowAnonymous]
        [Route("/category/{categoryId?}/subsection")]
        public async Task<IActionResult> Create(Guid? categoryId, [FromBody] SubsectionCreationModel model)
        {
            try
            {
                var createdSubsection = new SubsectionModel(new Description(model.Description), categoryId.Value);
                await _service.Create(createdSubsection);
                return StatusCode(200, new { createdSubsection });
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

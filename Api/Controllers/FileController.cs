using WebTutorialsApp.Common.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebTutorialsApp.Api.Models.Requests;
using WebTutorialsApp.Domain.Models;
using WebTutorialsApp.Domain.Services;
using WebTutorialsApp.Domain.ValueObjects;

namespace WebTutorialsApp.Api.Controllers
{
    [Authorize]
    [ApiController]
    public class FileController : Controller
    {
        private readonly IFileService _service;
        public FileController(IFileService service)
        {
            _service = service;
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("module/{moduleId?}/videoclass")]
        //public async Task<IActionResult> Create(Guid? moduleId, [FromBody] VideoClassCreationModel creationModel)
        //{
        //    try
        //    {
        //        var model = new FileModel(
        //                        new Sequence(creationModel.Sequence), 
        //                        new Description(creationModel.Description), 
        //                        moduleId.Value, new Bool(true)
        //                    );
        //        await _service.Create(model);
        //        return StatusCode(200, new { model });
        //    }
        //    catch (Exception exception)
        //    {
        //        if(exception is InvalidModelException)
        //        {
        //            var e = exception as InvalidModelException;
        //            return StatusCode(400, new { e.Notifications });
        //        }
        //        return StatusCode(500, $"{exception.Message}: {exception.InnerException}");                
        //    }
        //}
    }
}

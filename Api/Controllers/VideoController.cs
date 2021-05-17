using WebTutorialsApp.Api.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebTutorialsApp.Common.Exceptions;
using WebTutorialsApp.Domain.Models;
using WebTutorialsApp.Domain.Services;
using WebTutorialsApp.Domain.ValueObjects;
using WebTutorialsApp.Api.Models;

namespace WebTutorialsApp.Api.Controllers
{
    [Authorize]
    [ApiController]
    public class VideoController : Controller
    {
        private readonly IVideoService _service;

        public VideoController(IVideoService service)
        {
            _service = service;
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("/section/{sectionId?}/videos")]
        public async Task<IActionResult> GetSubsectionsByCategory(Guid? sectionId)
        {
            try
            {
                var subsections = await _service.GetBySection(sectionId);
                return StatusCode(200, new ResponseModel() { Data = subsections });
            }
            catch (Exception error)
            {
                return StatusCode(400, $"{error.Message} : {error.InnerException}");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/videos")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var videos = await _service.Get();
                return StatusCode(200, new ResponseModel() { Data = videos });
            }
            catch (Exception error)
            {
                return StatusCode(400, $"{error.Message} : {error.InnerException}");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/video/{id?}")]
        public async Task<IActionResult> Get(Guid? id)
        {
            try
            {
                var video = await _service.Get(id);
                return StatusCode(200, new ResponseModel() { Data = video });
            }
            catch (Exception error)
            {
                return StatusCode(400, $"{error.Message} : {error.InnerException}");
            }
        }
    }
}
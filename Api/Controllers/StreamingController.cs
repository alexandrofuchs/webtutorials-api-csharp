using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using WebTutorialsApp.Api.Filters;
using WebTutorialsApp.Api.Helpers;
using WebTutorialsApp.Api.Models.Requests;
using WebTutorialsApp.Api.WebServices;
using WebTutorialsApp.Domain.Entities;
using WebTutorialsApp.Domain.Models;
using WebTutorialsApp.Domain.Repositories;
using WebTutorialsApp.Domain.Services;
using WebTutorialsApp.Domain.ValueObjects;

namespace WebTutorialsApp.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class StreamingController : Controller
    {
        private readonly long _fileSizeLimit;
        private readonly ILogger<StreamingController> _logger;
        private readonly string[] _permittedExtensions = { ".mp4" };
        private readonly string _targetFilePath;
        private static readonly FormOptions _defaultFormOptions = new FormOptions();
        

        private readonly IFileProvider _fileProvider;
        IVideoRepository _repository;


       public StreamingController(ILogger<StreamingController> logger, IConfiguration config, IFileProvider fileProvider , IVideoRepository repository)
        {
            _logger = logger;
            _fileSizeLimit = config.GetValue<long>("FileSizeLimit");
            _targetFilePath = config.GetValue<string>("StoredFilesPath");
            _fileProvider = fileProvider;
            _repository = repository;
        }

        [HttpGet("video/{fileName?}/watch")]
        [AllowAnonymous]
        public IActionResult PlayVideoAsync(string fileName)
        {
            try
            {
                var file = _fileProvider.GetFileInfo(fileName);
                return new VideoStreamResult(new FileInfo(file.PhysicalPath).OpenRead(), "video/mp4");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [NonAction]
        public async Task CreateVideo(Video model)
        {
            await _repository.Create(model);
        }

        [HttpDelete]
        [Route("/video/{id?}/remove")]
        public async Task<IActionResult> RemoveVideo(Guid? id)
        {
            try
            {
                if (!id.HasValue)
                {
                    return BadRequest("invalid id!");
                }
                var video = await _repository.GetBy(id.Value);
                if (video == null)
                {
                    return NotFound();
                }
                var removeFile = _fileProvider.GetFileInfo(video.FileName);
                if (!removeFile.Exists)
                {
                    return NotFound();
                }
                await _repository.Delete(video);
                System.IO.File.Delete(removeFile.PhysicalPath);
                return Ok();

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            } 

        }

        [HttpPost]
        [Route("section/{sectionId?}/video/upload")]
        [DisableFormValueModelBinding]
        public async Task<IActionResult> UploadVideo(Guid? sectionId)

        {
            try
            {
                if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
                {
                    ModelState.AddModelError("File",
                        $"The request couldn't be processed (Error 1).");
                    // Log error

                    return BadRequest(ModelState);
                }

                var boundary = MultipartRequestHelper.GetBoundary(
                    MediaTypeHeaderValue.Parse(Request.ContentType),
                    _defaultFormOptions.MultipartBoundaryLengthLimit);
                var reader = new MultipartReader(boundary, HttpContext.Request.Body);
                var section = await reader.ReadNextSectionAsync();

                while (section != null)
                {
                    var hasContentDispositionHeader =
                        ContentDispositionHeaderValue.TryParse(
                            section.ContentDisposition, out var contentDisposition);

                    if (hasContentDispositionHeader)
                    {
                        if (!MultipartRequestHelper
                            .HasFileContentDisposition(contentDisposition))
                        {
                            ModelState.AddModelError("File",
                                $"The request couldn't be processed (Error 2).");


                            return BadRequest(ModelState);
                        }
                        else
                        {
                            var trustedFileNameForDisplay = WebUtility.HtmlEncode(
                             contentDisposition.FileName.Value);
                            var trustedFileNameForFileStorage = Path.GetRandomFileName();

                            var streamedFileContent = await FileHelpers.ProcessStreamedFile(
                                section, contentDisposition, ModelState,
                                _permittedExtensions, _fileSizeLimit);

                            if (!ModelState.IsValid)
                            {
                                return BadRequest(ModelState);
                            }

                            using (var targetStream = System.IO.File.Create(
                                Path.Combine(_targetFilePath, trustedFileNameForFileStorage)))
                            {
                                await targetStream.WriteAsync(streamedFileContent);

                                await CreateVideo(
                                    new Video(
                                        new Guid(), 
                                        DateTime.Now.ToString(), 
                                        DateTime.Now.ToString(), 
                                        trustedFileNameForFileStorage,
                                        trustedFileNameForDisplay, 
                                        _targetFilePath, sectionId.Value));

                                _logger.LogInformation(
                                    "Uploaded file '{TrustedFileNameForDisplay}' saved to " +
                                    "'{TargetFilePath}' as {TrustedFileNameForFileStorage}",
                                    trustedFileNameForDisplay, _targetFilePath,
                                    trustedFileNameForFileStorage);
                            }
                        }
                    }

                    section = await reader.ReadNextSectionAsync();
                }

                return Created(nameof(StreamingController), null);
            }
            catch (Exception e){
                return StatusCode(400, e.Message);
            }
        }


        private static Encoding GetEncoding(MultipartSection section)
        {
            var hasMediaTypeHeader =
                MediaTypeHeaderValue.TryParse(section.ContentType, out var mediaType);

            if (!hasMediaTypeHeader || Encoding.UTF7.Equals(mediaType.Encoding))
            {
                return Encoding.UTF8;
            }

            return mediaType.Encoding;
        }
    }
}


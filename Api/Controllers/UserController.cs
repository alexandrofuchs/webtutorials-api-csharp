using Microsoft.AspNetCore.Mvc;
using System;
using WebTutorialsApp.Domain.Models;
using WebTutorialsApp.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using WebTutorialsApp.Api.Models;
using WebTutorialsApp.Domain.ValueObjects;
using System.Threading.Tasks;
using WebTutorialsApp.Common.Exceptions;

namespace WebTutorialsApp.Api.Controllers
{
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserAuthenticationModel userAuthenticationModel)
        {
            try
            {
                var token = await _service.Authenticate(userAuthenticationModel.Email, userAuthenticationModel.Password);
                return StatusCode(200, new { token = token });
            }
            catch (Exception expection)
            {
                return StatusCode(400, $"{expection.Message}: {expection.InnerException}");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("user/register")]
        public async Task<IActionResult> Register([FromBody] UserCreationModel model)
        {
            try
            {
                var user = new UserModel(
                        new Name(model.FirstName, model.LastName),
                        new Email(model.Email),
                        new Password(model.Password)
                        );
                await _service.Register(user);
                user.Password.ClearPassword();
                return StatusCode(200, new
                {
                    createdUser = new
                    {
                        Id = user.Id,
                        Name = user.Name.FirstName + user.Name.LastName,
                        Email = user.Email.Value,
                        CreatedAt = user.CreatedAt.ToShortDateString(),
                        UpdatedAt = user.UpdatedAt.ToShortDateString()
                    }
                });
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

        [HttpGet]
        [Authorize]
        [Route("user/validatetoken")]     
        public async Task<IActionResult> ValidateToken()
        {
            try
            {
                return StatusCode(200, User.Identity.IsAuthenticated);
            }
            catch
            {
                return StatusCode(401, "unauthorized");
            }
        }
    }
}

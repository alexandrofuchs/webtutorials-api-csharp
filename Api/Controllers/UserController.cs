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
        [Route("/user")]
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


        [HttpGet]
        [Authorize]
        [Route("user/{id?}")]
        public async Task<IActionResult> GetById(Guid? id)
        {
            try
            {
                var user = await _service.GetBy(id.Value);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("user/{id?}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            try
            {
                var user = await _service.GetBy(id.Value);
                await _service.Delete(user);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

        [HttpPatch]
        [Authorize]
        [Route("user/{id?}")]
        public async Task<IActionResult> Update(Guid? id, [FromBody] UserUpdateModel model)
        {
            try
            {
                var user = await _service.GetBy(id.Value);
                if(user != null) {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Email = model.Email;
                    await _service.UpdateInformations(user);
                    return StatusCode(200);
                }
                return StatusCode(400);
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

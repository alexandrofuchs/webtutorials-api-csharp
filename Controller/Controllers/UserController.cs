using Microsoft.AspNetCore.Mvc;
using System;
using WebStreamingApplication.Domain.Models;
using WebStreamingApplication.Domain.Repositories;
using WebStreamingApplication.Persistence.Repositories;

namespace WebStreamingApplication.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        IUserRepository userRepository = new UserRepository();
    
        [HttpGet]
        public string Get(Guid id)
        {
            return "Hello World";
        }
    }
}

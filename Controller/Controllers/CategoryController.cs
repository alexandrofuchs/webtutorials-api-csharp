using Microsoft.AspNetCore.Mvc;
using System;
using WebStreamingApplication.Domain.Models;
using WebStreamingApplication.Domain.Repositories;
using WebStreamingApplication.Persistence.Repositories;

namespace WebStreamingApplication.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        ICategoryRepository userRepository = new CategoryRepository();

        [HttpGet]
        public CategoryModel Get(string description)
        {
            return userRepository.GetByDescription(description);
        }
    }
}

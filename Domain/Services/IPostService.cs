using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebTutorialsApp.Domain.Entities;
using WebTutorialsApp.Domain.Models;

namespace WebTutorialsApp.Domain.Services
{
    public interface IPostService : IDisposable
    {
        Task Create(PostModel model, Guid? subsectionId = null);        
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebTutorialsApp.Domain.Entities;
using WebTutorialsApp.Domain.Models;

namespace WebTutorialsApp.Domain.Services
{
    public interface IVideoService : IDisposable
    {  
        Task<IEnumerable<Video>> Get();
        Task<Video> Get(Guid? id);
        Task<IEnumerable<Video>> Get(int? pageIndex, int? maxItemsPerPage);
        Task<IEnumerable<Video>> GetBySection(Guid? sectionId);
    }
}

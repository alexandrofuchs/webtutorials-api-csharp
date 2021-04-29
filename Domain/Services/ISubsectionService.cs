using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebTutorialsApp.Domain.Entities;
using WebTutorialsApp.Domain.Models;

namespace WebTutorialsApp.Domain.Services
{
    public interface ISubsectionService : IDisposable
    {
        Task<Subsection> GetBy(Guid id);

        Task<int> GetPostsCount(Guid? subsectionId = null);

        Task<int> GetPostPagesCount(Guid? subsectionId = null, int? maxItemsPage = 5);

        Task Create(SubsectionModel model);               

        Task<IEnumerable<Subsection>> GetByCategory(Guid? categoryId=null);
    }
}

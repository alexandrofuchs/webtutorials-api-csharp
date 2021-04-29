using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebTutorialsApp.Common.Exceptions;
using WebTutorialsApp.Domain.Entities;
using WebTutorialsApp.Domain.Models;
using WebTutorialsApp.Domain.Repositories;
using WebTutorialsApp.Domain.Services;

namespace WebTutorialsApp.Middleware.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ISubsectionRepository _subsectionRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public PostService(IPostRepository postRepository, ISubsectionRepository subsectionRepository) : this(postRepository)
        {
            _subsectionRepository = subsectionRepository;
        }

        public PostService(IPostRepository postRepository, ISubsectionRepository subsectionRepository, ICategoryRepository categoryRepository) : this(postRepository, subsectionRepository)
        {
            _categoryRepository = categoryRepository;
        }





        //public async Task<IEnumerable<Post>> GetPostsWithFile(Guid? sectionId = null)
        //{
        //    if (!sectionId.HasValue)
        //    {
        //        throw new Exception("Invalid section Id!");
        //    }
        //    return await _postRepository.GetBySection(sectionId.Value);
        //}

        public async Task Create(PostModel model, Guid? subsectionId = null)
        {
            if (!subsectionId.HasValue)
            {
                throw new Exception("Invalid subsection!");
            }
            if (!model.IsModelValid())
            {
                throw new InvalidModelException(model.Notifications);
            }
            if (await _subsectionRepository.GetBy(model.SubsectionId) == null)
            {
                throw new Exception("Invalid subsection!");
            }
            if (await _postRepository.GetBy(model.Description.Value) != null)
            {
                throw new Exception("Post description already Exists!");
            }
            await _postRepository.Create(model.ToEntity());
        }

        public void Dispose()
        {
            _subsectionRepository?.Dispose();
            _postRepository?.Dispose();
        }
    }
}

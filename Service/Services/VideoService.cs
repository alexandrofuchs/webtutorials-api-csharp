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
    public class VideoService : IVideoService
    {
        private readonly IVideoRepository _videoRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ICategoryRepository _categoryRepository;

        public VideoService(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public VideoService(IVideoRepository videoRepository, ISectionRepository sectionRepository) : this(videoRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public VideoService(IVideoRepository videoRepository, ISectionRepository sectionRepository, ICategoryRepository categoryRepository) : this(videoRepository, sectionRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Video> Get(Guid? id)
        {
            if (!id.HasValue)
            {
                throw new Exception("invalid id");
            }
            var video = await _videoRepository.GetBy(id.Value);
            if(video == null)
            {
                throw new Exception("invalid id");
            }
            return video;
        }

        public async Task<IEnumerable<Video>> Get()
        {
            return await _videoRepository.GetVideos();            
        }

        public async Task<IEnumerable<Video>> GetBySection(Guid? sectionId)
        {
            if (!sectionId.HasValue)
            {
                throw new Exception("Invalid Section Id");
            }
            return await _videoRepository.GetBySection(sectionId.Value);
        }

        public async Task<IEnumerable<Video>> Get(int? pageIndex = 0, int? maxItemsPerPage = 10)
        {
            return await _videoRepository.Get(pageIndex, maxItemsPerPage);
        }

        public void Dispose()
        {
            _sectionRepository?.Dispose();
            _videoRepository?.Dispose();
        }
    }
}

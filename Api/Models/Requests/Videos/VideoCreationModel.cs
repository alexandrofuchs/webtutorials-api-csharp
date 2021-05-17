using System;

namespace WebTutorialsApp.Api.Models.Requests
{
    public class VideoCreationModel
    {
        public string Title { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public Guid? SectionId { get; set; }
    }
}


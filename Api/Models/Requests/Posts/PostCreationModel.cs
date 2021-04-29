using System;

namespace WebTutorialsApp.Api.Models.Requests
{
    public class PostCreationModel
    {
        public int Sequence { get; set; }
        public string Description { get; set; }
        public Guid? CourseId { get; set; }
    }
}

using System;

namespace WebTutorialsApp.Api.Models.Requests
{
    public class SubsectionCreationModel
    {
        public string Description { get; set; }
        public Guid? CategoryId { get; set; }
    }
}

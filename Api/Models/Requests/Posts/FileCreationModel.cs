using System;

namespace WebTutorialsApp.Api.Models.Requests
{
    public class FileCreationModel
    {
        public int Sequence { get; set; }
        public string Description { get; set; }
        public Guid? ModuleId { get; set; }
        public bool FreeAccess { get; set; }
    }
}

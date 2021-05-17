using System;
using WebTutorialsApp.Common.EntityClasses;

namespace WebTutorialsApp.Domain.Entities
{
    public class Video : EntityClass
    {
        #region PROPERTIES
        public string FileName { get; set; }
        public string StoragedFileName { get; set; }
        public string FilePath { get; set; }

        public Guid SectionId { get; set; }
        public Section Section { get; set; }


        #endregion PROPERTIES

        #region CONSTRUCTORS
        protected Video() { }
        public Video(Guid id, string createdAt, string updatedAt, string title, string storagedFileName, string filePath
            , Guid sectionId) : base(id, createdAt, updatedAt)
        {
            FileName = title;
            StoragedFileName = storagedFileName;
            FilePath = filePath;  
            SectionId = sectionId;    
        }
        #endregion CONSTRUCTORS

    }
}

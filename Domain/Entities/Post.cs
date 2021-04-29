using System;
using WebTutorialsApp.Common.EntityClasses;

namespace WebTutorialsApp.Domain.Entities
{
    public class Post : EntityClass
    {
        #region PROPERTIES
        public string Description { get; set; }
        public Guid SubsectionId { get; set; }
        public Subsection Subsection { get; set; }

        public PostFile File { get; set; }
        #endregion PROPERTIES

        #region CONSTRUCTORS
        protected Post() { }
        public Post(Guid id, DateTime createdAt, DateTime updatedAt, string description, Guid subsectionId) : base(id, createdAt, updatedAt)
        {
            Description = description;
            SubsectionId = subsectionId;    
        }
        #endregion CONSTRUCTORS

    }
}

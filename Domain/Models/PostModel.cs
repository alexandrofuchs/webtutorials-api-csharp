using System;
using WebTutorialsApp.Common.ModelClasses;
using WebTutorialsApp.Domain.Entities;
using WebTutorialsApp.Domain.ValueObjects;

namespace WebTutorialsApp.Domain.Models
{
    public class PostModel : ModelClass
    {
        #region PROPERTIES
        public Guid SubsectionId { get; private set; }
        public Guid FileId { get; set; }
        public Description Description { get; private set; }        
        #endregion PROPERTIES

        #region CONSTRUCTORS
        public PostModel(Description description, Guid subsectionId, Guid? id = null, DateTime? createdAt = null, DateTime? updatedAt = null)
            : base(id, createdAt, updatedAt)
        {
            SubsectionId = subsectionId;
            Description = description;
            AddNotifications(Description);
        }
        #endregion CONSTRUCTORS

        #region OVERRIDE METHODS
        public override Post ToEntity()
        => new(Id, CreatedAt, UpdatedAt, Description.Value, SubsectionId);
        #endregion OVERRIDE METHODS

    }
}

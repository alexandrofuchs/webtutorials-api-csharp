using System;
using System.Collections.Generic;
using WebTutorialsApp.Common.ModelClasses;
using WebTutorialsApp.Domain.Entities;
using WebTutorialsApp.Domain.ValueObjects;

namespace WebTutorialsApp.Domain.Models
{
    public class SubsectionModel : ModelClass
    {
        #region PROPERTIES
        public Description Description { get; private set; }
        public Guid CategoryId { get; private set; }
        #endregion PROPERTIES 

        #region CONSTRUCTORS
        public SubsectionModel(Description description, Guid categoryId, Guid? id = null, DateTime? createdAt = null, DateTime? updatedAt = null) : base(id, createdAt, updatedAt)
        {
            Description = description;
            CategoryId = categoryId;
            AddNotifications(Description);
        }
        #endregion CONSTRUCTORS

        #region METHODS
        public override Subsection ToEntity()
            => new(Id, CreatedAt, UpdatedAt, Description.Value, CategoryId);
        #endregion METHODS
    }
}

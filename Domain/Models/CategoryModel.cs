using System;
using WebTutorialsApp.Common.ModelClasses;
using WebTutorialsApp.Domain.Entities;
using WebTutorialsApp.Domain.ValueObjects;

namespace WebTutorialsApp.Domain.Models
{
    public class CategoryModel : ModelClass

    {
        #region PROPERTIES
        public Description Description { get; private set; }
        #endregion PROPERTIES

        #region CONSTRUCTORS
        public CategoryModel(Description description, Guid? id = null, DateTime? createdAt = null, DateTime? updatedAt = null)
            : base(id, createdAt, updatedAt)
        {
            Description = description;
            AddNotifications(Description);
        }
        #endregion CONSTRUCTORS   

        #region METHODS
        public override Category ToEntity()
            => new Category(Id, CreatedAt, OnUpdate, Description.Value);
        #endregion METHODS
    }
}

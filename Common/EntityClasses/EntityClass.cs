using WebTutorialsApp.Common.ModelClasses;
using System;

namespace WebTutorialsApp.Common.EntityClasses
{
    public abstract class EntityClass
    {
        #region PROPERTIES
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        #endregion PROPERTIES

        #region CONSTRUCTORS
        protected EntityClass() { }
        protected EntityClass(Guid id, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
        #endregion CONSTRUCTORS  
    }
}
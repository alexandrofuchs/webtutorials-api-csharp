using WebTutorialsApp.Common.ModelClasses;
using System;

namespace WebTutorialsApp.Common.EntityClasses
{
    public abstract class EntityClass
    {
        #region PROPERTIES
        public Guid Id { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        #endregion PROPERTIES

        #region CONSTRUCTORS
        protected EntityClass() { }
        protected EntityClass(Guid id, string createdAt, string updatedAt)
        {
            Id = id;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
        #endregion CONSTRUCTORS  
    }
}
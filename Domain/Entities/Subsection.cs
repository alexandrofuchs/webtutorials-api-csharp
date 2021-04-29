using System;
using System.Collections.Generic;
using WebTutorialsApp.Common.EntityClasses;

namespace WebTutorialsApp.Domain.Entities
{
    public class Subsection : EntityClass
    {
        #region PROPERTIES
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<Post> Posts{get;set;}
        #endregion PROPERTIES

        #region CONSTRUCTORS
        protected Subsection() : base() { }
        public Subsection(Guid id, DateTime createdAt, DateTime updatedAt, string description, Guid categoryId)
            : base(id, createdAt, updatedAt)
        {
            Description = description;
            CategoryId = categoryId; 
        }
        #endregion CONSTRUCTORS

    }
}

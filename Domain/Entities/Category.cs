using System;
using System.Collections.Generic;
using WebTutorialsApp.Common.EntityClasses;

namespace WebTutorialsApp.Domain.Entities
{
    public class Category : EntityClass
    {
        #region PROPERTIES
        public string Description { get; set; }

        public virtual List<Subsection> Subsections { get; set; }
        #endregion PROPERTIES

        #region CONSTRUCTORS
        protected Category() { }
        public Category(Guid id, DateTime createdAt, DateTime updatedAt, string description)
        : base(id, createdAt, updatedAt) => Description = description;
        #endregion CONSTRUCTORS

    }

}

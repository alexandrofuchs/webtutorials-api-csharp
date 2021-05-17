using System;
using System.Collections.Generic;
using WebTutorialsApp.Common.EntityClasses;

namespace WebTutorialsApp.Domain.Entities
{
    public class Category : EntityClass
    {
        #region PROPERTIES
        public string Description { get; set; }
        public virtual List<Section> Sections { get; set; }
        #endregion PROPERTIES

        #region CONSTRUCTORS
        protected Category() { }
        public Category(Guid id, string createdAt, string updatedAt, string description)
        : base(id, createdAt, updatedAt) => Description = description;
        #endregion CONSTRUCTORS

    }

}

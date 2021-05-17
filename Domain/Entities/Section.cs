using System;
using System.Collections.Generic;
using WebTutorialsApp.Common.EntityClasses;

namespace WebTutorialsApp.Domain.Entities
{
    public class Section : EntityClass
    {
        #region PROPERTIES
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<Video> Videos { get; set; }
        #endregion PROPERTIES

        #region CONSTRUCTORS
        protected Section() : base() { }
        public Section(Guid id, string createdAt, string updatedAt, string description, Guid categoryId)
            : base(id, createdAt, updatedAt)
        {
            Description = description;
            CategoryId = categoryId;
        }
        #endregion CONSTRUCTORS

    }
}

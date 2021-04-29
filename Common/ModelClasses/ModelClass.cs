using WebTutorialsApp.Common.EntityClasses;
using WebTutorialsApp.Common.Notifications;
using System;

namespace WebTutorialsApp.Common.ModelClasses
{
    public abstract class ModelClass : Notifiable<Notification>
    {
        #region PROPERTIES
        public Guid Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        #endregion PROPERTIES

        #region CONSTRUCTORS
        protected ModelClass(Guid? id = null, DateTime? createdAt = null, DateTime? updatedAt = null)
        {
            Id = id ?? Guid.NewGuid();
            CreatedAt = createdAt ?? DateTime.Now;
            UpdatedAt = updatedAt ?? DateTime.Now;
        }
        #endregion CONSTRUCTORS

        #region METHODS  
        protected static DateTime OnUpdate => DateTime.Now;
        public abstract EntityClass ToEntity();
        #endregion METHODS  

    }
}

using WebTutorialsApp.Common.EntityClasses;
using System;

namespace WebTutorialsApp.Domain.Entities
{
    public class User : EntityClass
    {
        #region PROPERTIES
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 
        public bool IsAdmin { get; set; }
        #endregion PROPERTIES

        #region CONSTRUCTORS
        protected User() : base() { }
        public User(Guid id, string createdAt, string updatedAt, string firstName, string lastName,
            string email, string password, bool isAdmin) : base(id, createdAt, updatedAt)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            IsAdmin = isAdmin;
        }
        #endregion CONSTRUCTORS

    }
}

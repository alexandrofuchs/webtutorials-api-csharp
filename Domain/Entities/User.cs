using WebTutorialsApp.Common.EntityClasses;
using WebTutorialsApp.Domain.Models;
using WebTutorialsApp.Domain.ValueObjects;
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
        public User(Guid id, DateTime createdAt, DateTime updatedAt, string firstName, string lastName,
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

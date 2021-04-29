using WebTutorialsApp.Common.ModelClasses;
using WebTutorialsApp.Domain.Entities;
using WebTutorialsApp.Domain.ValueObjects;
using System;

namespace WebTutorialsApp.Domain.Models
{
    public class UserModel : ModelClass
    {
        #region PROPERTIES
        public Name Name { get; private set; }
        public Email Email { get; private set; }
        public Password Password { get; private set; }
        public Bool IsAdmin { get; private set; } = new Bool(false);
        #endregion PROPERTIES

        #region CONSTRUCTORS

        public UserModel(Name name, Email email, Password password,
            Guid? guid = null, DateTime? createdAt = null, DateTime? updatedAt = null)
            : base(guid, createdAt, updatedAt)
        {
            Name = name;
            Email = email;
            Password = password;          

            AddNotifications(Name);
            AddNotifications(Email);
            AddNotifications(Password);
            AddNotifications(IsAdmin);
        }
        #endregion CONSTRUCTORS

        #region METHODS
        public void SetAdmin(Bool value)
        {

        }
        public void AlterInformations(Name name, Email email)
        {
            Name = name ?? Name;
            Email = email;

            AddNotifications(name);
            AddNotifications(email);
        }
        public override User ToEntity()
            => new (
                Id,
                CreatedAt,
                OnUpdate,
                Name.FirstName,
                Name.LastName,
                Email.Value,
                Password.Value,
                IsAdmin.Value
              );
        #endregion METHODS
    }
}

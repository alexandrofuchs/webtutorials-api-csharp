using WebTutorialsApp.Common.Contracts;
using WebTutorialsApp.Common.Notifications;
using WebTutorialsApp.Common.ValueObjects;

namespace WebTutorialsApp.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        #region PROPERTIES
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        #endregion

        #region CONSTRUCTORS
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            VerifyFirstName(firstName);
            VerifyLastName(lastName);
        }
        #endregion

        #region METHODS
        private void VerifyFirstName(string firstName)
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNullOrEmpty(firstName, "FirstName", "First Name cannot be null or empty") 
                .MaxAndMinLength(firstName, 3, 40, "FirstName", "First Name must be between 3-40 characters"));
        }

        private void VerifyLastName(string lastName)
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNullOrEmpty(lastName, "LastName", "Last Name cannot be null empty")
                .MaxAndMinLength(lastName, 3, 40, "LastName", "Last Name must be between 3-40 characters"));
        }

        public void Alter(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            VerifyFirstName(firstName);
            VerifyLastName(lastName);
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
        #endregion
    }
}

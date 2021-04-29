using WebTutorialsApp.Common.Contracts;
using WebTutorialsApp.Common.Notifications;
using WebTutorialsApp.Common.ValueObjects;

namespace WebTutorialsApp.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        #region PROPERTIES
        public string Value { get; private set; }
        #endregion

        #region CONSTRUCTORS
        public Email(string value)
        {
            Value = value;
            Verify();
        }
        #endregion

        #region METHODS
        private void Verify()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNullOrEmpty(Value, "Email", "Email cannot be null or empty!")
                .MaxLength(Value, 100, "Email", "Email cannot be longer than 100 characters!!")
                .IsValidEmail(Value, "Email", "Email is not valid!"));
        }

        public void Alter(string value)
        {
            Value = value;
            Verify();
        }
        #endregion METHODS
    }
}

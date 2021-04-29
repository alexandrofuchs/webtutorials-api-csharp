using WebTutorialsApp.Common.Contracts;
using WebTutorialsApp.Common.Notifications;
using WebTutorialsApp.Common.ValueObjects;

namespace WebTutorialsApp.Domain.ValueObjects
{
    public class Description : ValueObject
    {
        #region PROPERTIES
        public string Value { get; private set; }
        #endregion

        #region CONSTRUCTORS 
        public Description(string value)
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
                .IsNullOrEmpty(Value, "Description", "Description Value cannot be null!")
                .MaxAndMinLength(Value, 3, 50, "Description", "Description Value length cannot be longer than 50 characters!"));
        }
        public void Alter(string value)
        {
            Value = value;
            Verify();
        }
        #endregion
    }
}

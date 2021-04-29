using WebTutorialsApp.Common.Contracts;
using WebTutorialsApp.Common.Notifications;
using WebTutorialsApp.Common.ValueObjects;

namespace WebTutorialsApp.Domain.ValueObjects
{
    public class Password : ValueObject
    {
        #region PROPERTIES
        private string _value;

        public string Value
        {
            get { return _value; }
            private set { _value = value; }
        }
        #endregion PROPERTIES

        #region CONSTRUCTORS
        public Password(string value)
        {
            Value = value;
            VerifyPassword(value);
        }

        #endregion CONSTRUCTORS

        #region METHODS
        private void VerifyPassword(string value)
        {
            AddNotifications(new Contract<Notification>()
                    .Requires()
                    .IsNullOrEmpty(value, "Password", "Password value cannot be null or empty!")
                    .MinLength(value, 8, "Password", "Password length must have minimal eight characters!!")
                    .IsAlphanumeric(value, "Password", "Password value must contain numbers and letters!")
                    );
        }

        private void VerifyEncriptedPassword(string value)
        {
            AddNotifications(new Contract<Notification>()
                    .Requires()
                    .IsNullOrEmpty(value, "Password", "Password value cannot be null or empty!")
                    .MaxAndMinLength(value, 128, 128, "Password", "Invalid Password Length!")
                    );
        }

        public bool CheckPassword(string value) => _value == value;
        public void Alter(string value)
        {
            Value = value;
            VerifyPassword(Value);
        }
        public void OnEncrypt(string value)
        {
            Value = value;
            VerifyEncriptedPassword(Value);
        }
        public void ClearPassword()
        {
            Value = "";
        }
        #endregion  METHODS
    }
}

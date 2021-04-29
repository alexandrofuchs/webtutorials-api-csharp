using WebTutorialsApp.Common.Notifications;
using System.Text.RegularExpressions;
using System;

namespace WebTutorialsApp.Common.Contracts
{
    public partial class Contract<T> : Notifiable<Notification>
    {
        public Contract<T> IsNullOrEmpty(string value, string property, string message)
        {
            if (IsNull(value))
            {
                AddNotification(property, message);
                return this;
            }
            if (value.Trim().Length == 0)
            {
                AddNotification(property, message);
            }
            return this;
        }

        public Contract<T> MinLength(string value, int minLength, string property, string message)
        {
            if (IsNull(value) || value.Trim().Length < minLength)
            {
                AddNotification(property, message);
            }
            return this;
        }

        public Contract<T> MaxLength(string value, int maxLength, string property, string message)
        {
            if (IsNull(value) || value.Trim().Length > maxLength)
            {
                AddNotification(property, message);
            }
            return this;
        }

        public Contract<T> MaxAndMinLength(string value, int minLength, int maxLength, string property, string message)
        {

            if (!IsNull(value)) {
                var valueLength = value.Trim().Length;
                if (valueLength > maxLength || valueLength < minLength)
                {
                    AddNotification(property, message);                    
                }
                return this;
            }
            AddNotification(property, message);
            return this;
        }

        public Contract<T> IsAlphanumeric(string value, string property, string message)
        {
            Regex regex = new("^(?=.*[a-zA-Z])(?=.*[0-9])[A-Za-z0-9]+$");
            if (!IsNull(value) && !regex.IsMatch(value.Trim().ToLower()))
            {
                AddNotification(property, message);
                return this;
            }
            return this;
        }
    }
}

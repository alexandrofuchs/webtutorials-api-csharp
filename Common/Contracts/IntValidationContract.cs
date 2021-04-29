using WebTutorialsApp.Common.Notifications;
using System.Text.RegularExpressions;
using System;

namespace WebTutorialsApp.Common.Contracts
{
    public partial class Contract<T> : Notifiable<Notification>
    {
        public Contract<T> IsNumber(int value, string property, string message)
        {
            Regex regex = new(@"^[0-9]\d*$");
            if (!regex.IsMatch(value.ToString().Trim().ToLower()))
            {
                AddNotification(property, message);
                return this;
            }
            return this;
        }
    }
}

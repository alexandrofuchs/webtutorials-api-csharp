using WebTutorialsApp.Common.Notifications;

namespace WebTutorialsApp.Common.Contracts
{
    public partial class Contract<T> : Notifiable<Notification>
    {
        public Contract<T> IsValidEmail(string value, string property, string message)
        {
            if ( !IsNull(value) && value.Trim().ToLower().Contains("@"))
            {
                var splitedValue = value.Split("@");

                var part1 = splitedValue[0];
                var part2 = splitedValue[1];

                if (part1.Length <= 0 ||
                   part2.Length <= 0 ||
                   part1.Contains("@") ||
                   part2.Contains("@") ||
                   !part2.Contains("."))
                {
                    AddNotification(property, message);
                    return this;
                }

                var splitedDotsParts = part2.Split(".");

                foreach (var part in splitedDotsParts)
                {
                    if (part.Length <= 0)
                    {
                        AddNotification(property, message);
                        return this;
                    }
                }
                return this;
            }
            AddNotification(property, message);
            return this;
        }
    }
}

using WebTutorialsApp.Common.Notifications;

namespace WebTutorialsApp.Common.Contracts
{
    public partial class Contract<T> : Notifiable<Notification>
    {
        #region Fluent method invocation
        public Contract<T> Requires() => this; // returning the own object instance 

        private static bool IsNull(string value) => value == null;
        #endregion
    }
}

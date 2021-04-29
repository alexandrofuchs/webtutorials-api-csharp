namespace WebTutorialsApp.Common.Notifications
{
    public class Notification
    {
        #region PROPERTIES
        public string Key { get; set; }
        public string Message { get; set; }
        #endregion PROPERTIES

        #region CONSTRUCTORS
        public Notification(string key, string message)
        {
            Key = key;
            Message = message;
        }
        #endregion CONSTRUCTORS
    }
}

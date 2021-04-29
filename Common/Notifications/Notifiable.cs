using System;
using System.Collections.Generic;

namespace WebTutorialsApp.Common.Notifications
{
    public class Notifiable<T> where T : Notification
    {
        #region PROPERTIES
        private readonly List<T> _notifications;
        public IReadOnlyCollection<T> Notifications => _notifications;
        #endregion PROPERTIES

        #region CONSTRUCTORS
        protected Notifiable() => _notifications = new List<T>();
        #endregion CONSTRUCTORS

        #region METHODS
        private T GetNotificationInstance(string key, string message)
            => (T)Activator.CreateInstance(typeof(T), new object[] { key, message });
        public bool IsModelValid() => !(_notifications.Count > 0);  
        public void Clear() => _notifications.Clear();
        #endregion METHODS

        #region OVERLOADS ADDNOTIFICATION
        public void AddNotification(string key, string message)
            => _notifications.Add(GetNotificationInstance(key, message));
        public void AddNotification(T notification) =>
            _notifications.Add(notification);
        #endregion OVERLOADS ADDNOTIFICATION

        #region OVERLOADS ADDNOTIFICATIONS
        public void AddNotifications(IReadOnlyCollection<T> notifications)
            => _notifications.AddRange(notifications);
        public void AddNotifications(Notifiable<T> item)
         => AddNotifications(item.Notifications);
        #endregion OVERLOADS ADDNOTIFICATIONS       
    }
}
using System;
using System.Collections.Generic;
using WebTutorialsApp.Common.Notifications;

namespace WebTutorialsApp.Common.Exceptions
{
    public class InvalidModelException : Exception
    {
        public IEnumerable<Notification> Notifications { get; private set; }
        public InvalidModelException(IEnumerable<Notification> notifications)
        {
            Notifications = notifications;
        }
    }
}

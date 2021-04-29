using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTutorialsApp.Common.Notifications;

namespace Api.Models.Responses
{
    public class ErrorResponseModel
    {
        public IEnumerable<Notification> Notifications { get; private set; }

        public ErrorResponseModel(IEnumerable<Notification> notifications)
        {
            Notifications = notifications;
        }  
    }   
}

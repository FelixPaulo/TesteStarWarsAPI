using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StarWars.Application.Notifications
{
    public class ApplicationNotificationHandler : INotificationHandler<ApplicationNotification>
    {
        private List<ApplicationNotification> _notifications;

        public ApplicationNotificationHandler()
        {
            _notifications = new List<ApplicationNotification>();
        }

        public virtual List<ApplicationNotification> GetNotifications()
        {
            return _notifications;
        }

        public Task Handle(ApplicationNotification notification, CancellationToken cancellationToken)
        {
            _notifications.Add(notification);
            return Task.CompletedTask;
        }

        public virtual bool HasNotifications()
        {
            return _notifications.Any();
        }

        public void Dispose()
        {
            _notifications = new List<ApplicationNotification>();
        }
    }
}

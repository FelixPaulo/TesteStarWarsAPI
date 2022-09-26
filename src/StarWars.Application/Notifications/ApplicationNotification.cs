using StarWars.Application.Events;
using System;

namespace StarWars.Application.Notifications
{
    public class ApplicationNotification : Event
    {
        public Guid NotificationId { get; private set; }
        public string Value { get; private set; }
        public int Version { get; private set; }

        public ApplicationNotification(string value)
        {
            NotificationId = Guid.NewGuid();
            Value = value;
            Version = 1;
        }
    }
}

using System;

namespace StarWars.Application.Events
{
    public class Message
    {
        public string MessageType { get; protected set; }
        public Guid AggregateId { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
            AggregateId = Guid.NewGuid();
        }
    }
}

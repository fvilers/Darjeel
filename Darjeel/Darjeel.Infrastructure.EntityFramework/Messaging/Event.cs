using System;

namespace Darjeel.Infrastructure.EntityFramework.Messaging
{
    public class Event : Message
    {
        private Event()
        {
            // Required for Entity Framework
        }

        public Event(string body, DateTime? deliveryDate = null, string correlationId = null)
            : base(body, deliveryDate, correlationId)
        {
        }
    }
}
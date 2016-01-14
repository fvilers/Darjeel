using System;

namespace Darjeel.Infrastructure.EntityFramework.Messaging
{
    public class EventEntity : MessageEntity
    {
        private EventEntity()
        {
            // Required for Entity Framework
        }

        public EventEntity(string body, DateTime? deliveryDate = null, string correlationId = null)
            : base(body, deliveryDate, correlationId)
        {
        }
    }
}
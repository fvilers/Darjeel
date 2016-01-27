using System;

namespace Darjeel.EntityFramework.Messaging
{
    public class EventEntity : MessageEntity
    {
        // ReSharper disable once UnusedMember.Local
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
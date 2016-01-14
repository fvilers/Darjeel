using System;

namespace Darjeel.Infrastructure.EntityFramework.Messaging
{
    public class MessageEntity
    {
        public Guid Id { get; private set; }
        public string Body { get; private set; }
        public string CorrelationId { get; private set; }
        public DateTime? DeliveryDate { get; private set; }

        protected MessageEntity()
        {
            // Required for Entity Framework
        }

        protected MessageEntity(string body, DateTime? deliveryDate = null, string correlationId = null)
        {
            Id = Guid.NewGuid();
            Body = body;
            DeliveryDate = deliveryDate;
            CorrelationId = correlationId;
        }
    }
}

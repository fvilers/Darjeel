using System;

namespace Darjeel.Infrastructure.EntityFramework.Messaging
{
    public abstract class MessageEntity
    {
        public Guid Id { get; private set; }
        public string Body { get; private set; }
        public string CorrelationId { get; private set; }
        public DateTime? DeliveryDate { get; private set; }

        protected MessageEntity()
        {
            Id = Guid.NewGuid();
        }

        protected MessageEntity(string body, DateTime? deliveryDate = null, string correlationId = null)
            : this()
        {
            Body = body;
            DeliveryDate = deliveryDate;
            CorrelationId = correlationId;
        }
    }
}

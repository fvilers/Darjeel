using Darjeel.Infrastructure.EventSourcing;
using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Darjeel.Infrastructure.Azure.EventSourcing
{
    public class EventEntity : TableEntity
    {
        public string AggregateType { get; set; }
        public string Payload { get; set; }
        public string CorrelationId { get; set; }

        public EventEntity()
        {
            // Required for Azure table storage
        }

        public EventEntity(StoredEvent storedEvent)
            : this()
        {
            AggregateId = storedEvent.AggregateId;
            AggregateType = storedEvent.AggregateType;
            Version = storedEvent.Version;
            Payload = storedEvent.Payload;
            CorrelationId = storedEvent.CorrelationId;
        }

        [IgnoreProperty]
        public Guid AggregateId
        {
            get { return Guid.Parse(PartitionKey); }
            set { PartitionKey = value.ToString(); }
        }

        [IgnoreProperty]
        public int Version
        {
            get { return Int32.Parse(RowKey); }
            set { RowKey = value.ToString("D10"); }
        }

        public StoredEvent ToStoredEvent()
        {
            return new StoredEvent
            {
                AggregateId = AggregateId,
                AggregateType = AggregateType,
                Version = Version,
                Payload = Payload,
                CorrelationId = CorrelationId
            };
        }
    }
}
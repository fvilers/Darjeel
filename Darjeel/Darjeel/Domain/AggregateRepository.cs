using Darjeel.Diagnostics.Extensions;
using Darjeel.EventSourcing;
using Darjeel.Messaging;
using Darjeel.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Darjeel.Domain
{
    public class AggregateRepository<T> : IAggregateRepository<T>
        where T : AggregateRoot
    {
        private static readonly string AggregateType = typeof(T).Name;
        private readonly ITextSerializer _serializer;
        private readonly IEventStore _store;
        private readonly IEventBus _eventBus;

        public AggregateRepository(ITextSerializer serializer, IEventStore store, IEventBus eventBus)
        {
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));
            if (store == null) throw new ArgumentNullException(nameof(store));
            if (eventBus == null) throw new ArgumentNullException(nameof(eventBus));
            _serializer = serializer;
            _store = store;
            _eventBus = eventBus;
        }

        public async Task<T> GetAsync(Guid aggregateId)
        {
            if (aggregateId == Guid.Empty) throw new ArgumentNullException(nameof(aggregateId));

            var events = await _store.FindAsync(aggregateId);
            var payloads = events
                .Select(x => DeserializeObject(x.Payload))
                .OrderBy(x => x.Version)
                .ToArray();

            if (!payloads.Any())
            {
                Logging.Darjeel.TraceInformation($"No events found for aggregate ID '{aggregateId}'.");
                return null;
            }

            var parameters = new object[]
            {
                aggregateId,
                payloads
            };
            var constructor = typeof(T).GetConstructor(new[] { typeof(Guid), typeof(IEnumerable<IVersionedEvent>) });

            if (constructor == null)
            {
                Logging.Darjeel.TraceError("Missing ctor to load aggregate's history.");
                throw new InvalidOperationException("Missing ctor to load aggregate's history.");
            }

            var aggregate = (T)constructor.Invoke(parameters);

            return aggregate;
        }

        public async Task StoreAsync(T aggregate, string correlationId = null)
        {
            if (aggregate == null) throw new ArgumentNullException(nameof(aggregate));

            var events = aggregate.UncommittedEvents.ToArray();
            var aggregateId = aggregate.Id; // avoids to capture closure on aggregate
            var correlationIdForEnvelope = correlationId; // avoids to capture closure on this
            var storedEvents = events.Select(@event => new StoredEvent
            {
                AggregateId = aggregateId,
                AggregateType = AggregateType,
                Version = @event.Version,
                Payload = SerializeObject(@event),
                CorrelationId = correlationId
            }).ToArray();
            var envelopes = events.Select(@event => new Envelope<IEvent>(@event)
            {
                CorrelationId = correlationIdForEnvelope
            });

            await _store.SaveAsync(storedEvents);
            await _eventBus.PublishAsync(envelopes);
        }

        private string SerializeObject(IEvent e)
        {
            using (var writer = new StringWriter())
            {
                _serializer.Serialize(writer, e);

                return writer.ToString();
            }
        }

        private IVersionedEvent DeserializeObject(string payload)
        {
            using (var reader = new StringReader(payload))
            {
                return (IVersionedEvent)_serializer.Deserialize(reader);
            }
        }
    }
}
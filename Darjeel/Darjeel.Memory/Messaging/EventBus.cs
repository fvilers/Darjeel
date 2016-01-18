using Darjeel.Diagnostics.Extensions;
using Darjeel.Messaging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Darjeel.Memory.Messaging
{
    public class EventBus : IEventBus
    {
        private readonly IProducerConsumerCollection<Envelope<IEvent>> _eventCollection;

        public EventBus(IProducerConsumerCollection<Envelope<IEvent>> eventCollection)
        {
            if (eventCollection == null) throw new ArgumentNullException(nameof(eventCollection));
            _eventCollection = eventCollection;
        }

        public Task PublishAsync(Envelope<IEvent> envelope)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));

            if (!_eventCollection.TryAdd(envelope))
            {
                Logging.DarjeelMemory.TraceError("Sending envelope failed.");
                throw new Exception("Publishing envelope failed.");
            }

            return Task.FromResult(0);
        }

        public async Task PublishAsync(IEnumerable<Envelope<IEvent>> envelopes)
        {
            if (envelopes == null) throw new ArgumentNullException(nameof(envelopes));

            foreach (var envelope in envelopes)
            {
                await PublishAsync(envelope);
            }
        }
    }
}

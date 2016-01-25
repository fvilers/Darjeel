using Darjeel.Messaging;
using Darjeel.Messaging.Handling;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Darjeel.Memory.Messaging
{
    public class InProcEventBus : IEventBus
    {
        private readonly IEventDispatcher _dispatcher;

        public InProcEventBus(IEventDispatcher dispatcher)
        {
            if (dispatcher == null) throw new ArgumentNullException(nameof(dispatcher));
            _dispatcher = dispatcher;
        }

        public async Task PublishAsync(Envelope<IEvent> envelope)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));

            await _dispatcher.DispatchEventAsync(envelope.Body, envelope.CorrelationId);
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
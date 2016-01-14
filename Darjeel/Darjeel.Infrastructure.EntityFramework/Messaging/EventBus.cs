using Darjeel.Infrastructure.Messaging;
using Darjeel.Infrastructure.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Darjeel.Infrastructure.EntityFramework.Messaging
{
    public class EventBus : IEventBus
    {
        private readonly IBusContext _context;
        private readonly ITextSerializer _serializer;

        public EventBus(IBusContext context, ITextSerializer serializer)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));
            _context = context;
            _serializer = serializer;
        }

        public async Task PublishAsync(Envelope<IEvent> envelope)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));

            var message = BuildEvent(envelope);

            _context.Events.Add(message);
            await _context.SaveChangesAsync();
        }

        public async Task PublishAsync(IEnumerable<Envelope<IEvent>> envelopes)
        {
            if (envelopes == null) throw new ArgumentNullException(nameof(envelopes));

            foreach (var message in envelopes.Select(BuildEvent))
            {
                _context.Events.Add(message);
            }

            await _context.SaveChangesAsync();
        }

        private EventEntity BuildEvent(Envelope<IEvent> @event)
        {
            using (var payloadWriter = new StringWriter())
            {
                _serializer.Serialize(payloadWriter, @event.Body);

                return new EventEntity(payloadWriter.ToString(), correlationId: @event.CorrelationId);
            }
        }
    }
}

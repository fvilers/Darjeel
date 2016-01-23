using Darjeel.Messaging;
using Darjeel.Messaging.Handling;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Darjeel.Memory.Messaging
{
    public class InProcEventBus : IEventBus
    {
        private readonly IEventHandlerRegistry _registry;

        public InProcEventBus(IEventHandlerRegistry registry)
        {
            if (registry == null)
            {
                throw new ArgumentNullException(nameof(registry));
            }
            _registry = registry;
        }

        public async Task PublishAsync(Envelope<IEvent> envelope)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));

            var message = envelope.Body;
            var eventType = message.GetType();
            IEnumerable<IEventHandler> handlers;
            var tasks = new List<Task>();

            if (_registry.TryGetHandlers(eventType, out handlers))
            {
                foreach (var handler in handlers)
                {
                    Logging.DarjeelMemory.TraceInformation($"Event '{eventType.FullName}' handled by '{handler.GetType().FullName}.");
                    var task = ((dynamic)handler).HandleAsync((dynamic)message);
                    tasks.Add(task);
                }
            }
            else if (_registry.TryGetHandlers(typeof(IEvent), out handlers))
            {
                foreach (var handler in handlers)
                {
                    Logging.DarjeelMemory.TraceInformation($"Event '{eventType.FullName}' handled by '{handler.GetType().FullName}.");
                    var task = ((dynamic)handler).Handle((dynamic)message);
                    tasks.Add(task);
                }
            }

            await Task.WhenAll(tasks);
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
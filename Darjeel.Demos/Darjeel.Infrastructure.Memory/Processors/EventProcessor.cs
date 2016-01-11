using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using Darjeel.Infrastructure.Messaging;
using Darjeel.Infrastructure.Messaging.Handling;

namespace Darjeel.Infrastructure.Memory.Processors
{
    public class EventProcessor : MessageProcessor<IEvent>
    {
        private readonly IEventHandlerRegistry _registry;

        public EventProcessor(IEventHandlerRegistry registry, IProducerConsumerCollection<Envelope<IEvent>> commandCollection)
            : base(commandCollection)
        {
            if (registry == null) throw new ArgumentNullException(nameof(registry));
            _registry = registry;
        }

        protected override void ProcessMessage(IEvent message, string correlationId)
        {
            var eventType = message.GetType();
            IEnumerable<IEventHandler> handlers;

            if (_registry.TryGetHandlers(eventType, out handlers))
            {
                foreach (var handler in handlers)
                {
                    Trace.TraceInformation("Event '{0}' handled by '{1}.", eventType.FullName, handler.GetType().FullName);
                    ((dynamic)handler).Handle((dynamic)message);
                }
            }
            else if (_registry.TryGetHandlers(typeof(ICommand), out handlers))
            {
                foreach (var handler in handlers)
                {
                    Trace.TraceInformation("Event '{0}' handled by '{1}.", eventType.FullName, handler.GetType().FullName);
                    ((dynamic)handler).Handle((dynamic)message);
                }
            }
        }
    }
}
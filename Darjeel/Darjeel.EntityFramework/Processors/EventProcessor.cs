using Darjeel.EntityFramework.Messaging;
using Darjeel.EventSourcing;
using Darjeel.Messaging;
using Darjeel.Messaging.Handling;
using Darjeel.Serialization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Darjeel.EntityFramework.Processors
{
    public class EventProcessor : MessageProcessor<EventEntity>
    {
        private readonly IEventHandlerRegistry _registry;

        public EventProcessor(IEventHandlerRegistry registry, Func<IBusContext> busContextFactory, ITextSerializer serializer)
            : base(busContextFactory, serializer)
        {
            if (registry == null) throw new ArgumentNullException(nameof(registry));
            _registry = registry;
        }

        protected override async Task ProcessMessageAsync(object message, string correlationId)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            var versionedEvent = message as IVersionedEvent;
            message = versionedEvent?.Event ?? message;

            var eventType = message.GetType();
            IEnumerable<IEventHandler> handlers;
            var tasks = new List<Task>();

            if (_registry.TryGetHandlers(eventType, out handlers))
            {
                foreach (var handler in handlers)
                {
                    Logging.DarjeelEntityFramework.TraceInformation($"Event '{eventType.FullName}' handled by '{handler.GetType().FullName}.");
                    var task = ((dynamic)handler).HandleAsync((dynamic)message);
                    tasks.Add(task);
                }
            }
            else if (_registry.TryGetHandlers(typeof(ICommand), out handlers))
            {
                foreach (var handler in handlers)
                {
                    Logging.DarjeelEntityFramework.TraceInformation($"Event '{eventType.FullName}' handled by '{handler.GetType().FullName}.");
                    var task = ((dynamic)handler).Handle((dynamic)message);
                    tasks.Add(task);
                }
            }

            await Task.WhenAll(tasks);
        }
    }
}
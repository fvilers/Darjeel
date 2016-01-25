using Darjeel.EntityFramework.Messaging;
using Darjeel.Messaging;
using Darjeel.Messaging.Handling;
using Darjeel.Serialization;
using System;
using System.Threading.Tasks;

namespace Darjeel.EntityFramework.Processors
{
    public class EventProcessor : MessageProcessor<EventEntity>
    {
        private readonly IEventDispatcher _dispatcher;

        public EventProcessor(IEventDispatcher dispatcher, Func<IBusContext> busContextFactory, ITextSerializer serializer)
            : base(busContextFactory, serializer)
        {
            if (dispatcher == null) throw new ArgumentNullException(nameof(dispatcher));
            _dispatcher = dispatcher;
        }

        protected override async Task ProcessMessageAsync(object message, string correlationId)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            await _dispatcher.DispatchEventAsync((IEvent)message);
        }
    }
}
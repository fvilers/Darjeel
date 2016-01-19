using Darjeel.EventSourcing;
using Darjeel.Messaging;
using System;
using System.Collections.Generic;

namespace Darjeel.Domain
{
    public abstract class AggregateRoot : IAggregateRoot, IEventSourced
    {
        public Guid Id { get; }
        public int Version { get; private set; }
        public IEnumerable<IVersionedEvent> UncommittedEvents => _uncommittedEvents;

        private readonly Dictionary<Type, Action<IVersionedEvent>> _handlers = new Dictionary<Type, Action<IVersionedEvent>>();
        private readonly List<IVersionedEvent> _uncommittedEvents = new List<IVersionedEvent>();

        protected AggregateRoot(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));
            Id = id;
        }

        protected void Handle<T>(Action<T> handler)
            where T : IEvent
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            _handlers.Add(typeof(T), @event => handler((T)@event));
        }

        protected void Raise(IVersionedEvent @event)
        {
            if (@event == null) throw new ArgumentNullException(nameof(@event));

            @event.SourceId = Id;
            @event.Version = Version + 1;

            Raise(@event, true);
        }

        protected void ReplayHistory(IEnumerable<IVersionedEvent> history)
        {
            if (history == null) throw new ArgumentNullException(nameof(history));

            foreach (var @event in history)
            {
                Raise(@event);
            }
        }

        private void Raise(IVersionedEvent @event, bool isNew)
        {
            Action<IVersionedEvent> handler;
            if (_handlers.TryGetValue(@event.GetType(), out handler))
            {
                handler(@event);
            }

            Version = @event.Version;

            if (isNew)
            {
                _uncommittedEvents.Add(@event);
            }
        }
    }
}

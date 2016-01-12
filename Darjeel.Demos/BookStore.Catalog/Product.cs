using Darjeel.Infrastructure.Domain;
using Darjeel.Infrastructure.EventSourcing;
using System;
using System.Collections.Generic;
using BookStore.Catalog.Events;

namespace BookStore.Catalog
{
    public class Product : AggregateRoot
    {
        public string Title { get; private set; }

        public Product(string title)
            : this(Guid.NewGuid())
        {
            if (title == null) throw new ArgumentNullException(nameof(title));

            Raise(new ProductCreated(Id, title));
        }

        public Product(Guid id, IEnumerable<IVersionedEvent> history)
            : this(id)
        {
            ReplayHistory(history);
        }

        protected Product(Guid id)
            : base(id)
        {
            Handle<ProductCreated>(OnProductCreated);
        }

        private void OnProductCreated(ProductCreated @event)
        {
            if (@event == null) throw new ArgumentNullException(nameof(@event));
            Title = @event.Title;
        }
    }
}

using Darjeel.Infrastructure.Domain;
using Darjeel.Infrastructure.EventSourcing;
using System;
using System.Collections.Generic;

namespace BookStore.Catalog
{
    public class Product : AggregateRoot
    {
        public string Title { get; private set; }

        public Product(string title)
            : this(Guid.NewGuid())
        {
            if (title == null) throw new ArgumentNullException(nameof(title));
        }

        public Product(Guid id, IEnumerable<IVersionedEvent> history)
            : this(id)
        {
            ReplayHistory(history);
        }

        protected Product(Guid id)
            : base(id)
        {
        }
    }
}

using Darjeel.Messaging;
using System;

namespace BookStore.Catalog.Events
{
    public class ProductCreated : Event
    {
        public string Title { get; private set; }

        public ProductCreated(Guid sourceId, string title)
            : base(sourceId)
        {
            Title = title;
        }
    }
}

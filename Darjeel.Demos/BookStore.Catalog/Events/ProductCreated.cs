using Darjeel.EventSourcing;

namespace BookStore.Catalog.Events
{
    public class ProductCreated : VersionedEvent
    {
        public string Title { get; private set; }

        public ProductCreated(string title)
        {
            Title = title;
        }
    }
}

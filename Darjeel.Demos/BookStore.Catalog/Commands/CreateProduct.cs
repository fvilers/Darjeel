using Darjeel.Infrastructure.Messaging;

namespace BookStore.Catalog.Commands
{
    public class CreateProduct : Command
    {
        public string Title { get; set; }
    }
}

using BookStore.Catalog.Commands;
using Darjeel.Domain;
using Darjeel.Messaging.Handling;
using System;
using System.Threading.Tasks;

namespace BookStore.Catalog.CommandHandlers
{
    public class ProductCommandHandler : ICommandHandler<CreateProduct>
    {
        private readonly IAggregateRepository<Product> _repository;

        public ProductCommandHandler(IAggregateRepository<Product> repository)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            _repository = repository;
        }

        public async Task HandleAsync(CreateProduct command)
        {
            var product = new Product(command.Title);
            await _repository.StoreAsync(product, command.Id.ToString());
        }
    }
}

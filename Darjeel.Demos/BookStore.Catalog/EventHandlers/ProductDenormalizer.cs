using BookStore.Catalog.Events;
using BookStore.Catalog.ReadModels;
using Darjeel.Messaging.Handling;
using System;
using System.Threading.Tasks;

namespace BookStore.Catalog.EventHandlers
{
    public class ProductDenormalizer : IEventHandler<ProductCreated>
    {
        private readonly IReadModelProductDao _dao;

        public ProductDenormalizer(IReadModelProductDao dao)
        {
            if (dao == null) throw new ArgumentNullException(nameof(dao));
            _dao = dao;
        }

        public async Task HandleAsync(ProductCreated @event)
        {
            if (@event == null) throw new ArgumentNullException(nameof(@event));

            var product = await _dao.GetAsync(@event.SourceId) ?? _dao.Add(@event.SourceId);
            product.Title = @event.Title;

            await _dao.SaveChangesAsync();
        }
    }
}

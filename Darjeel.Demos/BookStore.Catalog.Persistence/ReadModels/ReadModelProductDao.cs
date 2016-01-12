using BookStore.Catalog.ReadModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Catalog.Persistence.ReadModels
{
    public class ReadModelProductDao : IReadModelProductDao
    {
        private readonly IReadModelContext _context;

        public ReadModelProductDao(IReadModelContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            _context = context;
        }

        public async Task<IEnumerable<IReadModelProduct>> FindAsync()
        {
            var query = from x in _context.Products
                        select x;
            var products = await query.ToArrayAsync();

            return products;
        }

        public async Task<IReadModelProduct> GetAsync(Guid aggregateId)
        {
            if (aggregateId == Guid.Empty) throw new ArgumentNullException(nameof(aggregateId));

            var query = from x in _context.Products
                        where x.AggregateId == aggregateId
                        select x;
            var product = await query.FirstOrDefaultAsync();

            return product;
        }

        public IReadModelProduct Add(Guid aggregateId)
        {
            if (aggregateId == Guid.Empty) throw new ArgumentNullException(nameof(aggregateId));

            var product = _context.Products.Add(new ReadModelProduct
            {
                AggregateId = aggregateId
            });

            return product;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

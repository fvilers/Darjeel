using BookStore.Catalog.ReadModels;
using System;

namespace BookStore.Catalog.Persistence.ReadModels
{
    public class Product : IProduct
    {
        public Guid AggregateId { get; set; }
        public string Title { get; set; }
    }
}

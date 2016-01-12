using System;

namespace BookStore.Catalog.ReadModels
{
    public interface IReadModelProduct
    {
        Guid AggregateId { get; set; }
        string Title { get; set; }
    }
}
using System;

namespace BookStore.Catalog.ReadModels
{
    public interface IProduct
    {
        Guid AggregateId { get; set; }
        string Title { get; set; }
    }
}
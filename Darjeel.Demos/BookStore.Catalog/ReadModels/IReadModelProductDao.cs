using Darjeel.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Catalog.ReadModels
{
    public interface IReadModelProductDao : IUnitOfWork
    {
        Task<IEnumerable<IReadModelProduct>> FindAsync();
        Task<IReadModelProduct> GetAsync(Guid aggregateId);
        IReadModelProduct Add(Guid aggregateId);
    }
}
using System;
using System.Threading.Tasks;

namespace Darjeel.Domain
{
    public interface IAggregateRepository<T>
        where T : IAggregateRoot
    {
        Task<T> GetAsync(Guid aggregateId);
        Task StoreAsync(T aggregate, string correlationId = null);
    }
}
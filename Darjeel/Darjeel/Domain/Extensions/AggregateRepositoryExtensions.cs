using System;
using System.Threading.Tasks;

namespace Darjeel.Domain.Extensions
{
    public static class AggregateRepositoryExtensions
    {
        public static Task StoreAsync<T>(this IAggregateRepository<T> repository, T aggregate, Guid correlationId)
            where T : IAggregateRoot
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            if (aggregate == null) throw new ArgumentNullException(nameof(aggregate));

            return repository.StoreAsync(aggregate, correlationId.ToString());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Darjeel.EventSourcing
{
    public interface IEventStore
    {
        Task<IEnumerable<StoredEvent>> FindAsync(Guid aggregateId);
        Task SaveAsync(IEnumerable<StoredEvent> events);
    }
}
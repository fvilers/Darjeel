using Darjeel.Infrastructure.EventSourcing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Darjeel.Infrastructure.EntityFramework.EventSourcing
{
    public class EventStore : IEventStore
    {
        public Task<IEnumerable<StoredEvent>> FindAsync(Guid aggregateId)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(IEnumerable<StoredEvent> events)
        {
            throw new NotImplementedException();
        }
    }
}

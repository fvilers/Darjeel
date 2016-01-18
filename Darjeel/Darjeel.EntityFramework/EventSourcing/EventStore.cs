using Darjeel.EventSourcing;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Darjeel.EntityFramework.EventSourcing
{
    public class EventStore : IEventStore
    {
        private readonly IEventContext _context;

        public EventStore(IEventContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            _context = context;
        }

        public async Task<IEnumerable<StoredEvent>> FindAsync(Guid aggregateId)
        {
            var query = from x in _context.Events
                        where x.AggregateId == aggregateId
                        select x;
            var results = await query.ToArrayAsync();

            return results;
        }

        public async Task SaveAsync(IEnumerable<StoredEvent> events)
        {
            foreach (var e in events)
            {
                _context.Events.Add(e);
            }

            await _context.SaveChangesAsync();
        }
    }
}

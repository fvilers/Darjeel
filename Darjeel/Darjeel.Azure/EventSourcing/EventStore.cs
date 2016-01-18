using Darjeel.Azure.EventSourcing.Extensions;
using Darjeel.EventSourcing;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Darjeel.Azure.EventSourcing
{
    public class EventStore : IEventStore
    {
        private const string TableName = "EventStoreEvents";
        private readonly Func<CloudTableClient> _tableClientFactory;

        public EventStore(Func<CloudTableClient> tableClientFactory)
        {
            if (tableClientFactory == null) throw new ArgumentNullException(nameof(tableClientFactory));
            _tableClientFactory = tableClientFactory;
        }

        public async Task<IEnumerable<StoredEvent>> FindAsync(Guid aggregateId)
        {
            if (aggregateId == Guid.Empty) throw new ArgumentNullException(nameof(aggregateId));

            var tableClient = _tableClientFactory();
            var table = tableClient.GetTableReference(TableName);
            table.CreateIfNotExists();

            var query = new TableQuery<EventEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, aggregateId.ToString()));
            var entities = await table.ExecuteQueryAsync(query);
            var events = entities.Select(x => x.ToStoredEvent());

            return events;
        }

        public async Task SaveAsync(IEnumerable<StoredEvent> events)
        {
            if (events == null) throw new ArgumentNullException(nameof(events));

            var tableClient = _tableClientFactory();
            var table = tableClient.GetTableReference(TableName);
            table.CreateIfNotExists();

            var operations = events.Select(e => new EventEntity(e)).Select(TableOperation.Insert);

            foreach (var insertOperation in operations)
            {
                await table.ExecuteAsync(insertOperation);
            }
        }
    }
}

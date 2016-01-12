using Darjeel.Infrastructure.EntityFramework.EventSourcing.Migrations;
using Darjeel.Infrastructure.EventSourcing;
using System;
using System.Data.Entity;

namespace Darjeel.Infrastructure.EntityFramework.EventSourcing
{
    public class EventContext : ContextBase, IEventContext
    {
        public IDbSet<StoredEvent> Events { get; set; }

        private const string SchemaName = "eventstore";

        public EventContext()
            : base("EventStore")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new ArgumentNullException(nameof(modelBuilder));

            modelBuilder.Configurations.Add(new StoredEventConfiguration(SchemaName));
        }
    }
}
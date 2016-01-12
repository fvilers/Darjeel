using Darjeel.Infrastructure.EventSourcing;
using System;
using System.Data.Entity.ModelConfiguration;

namespace Darjeel.Infrastructure.EntityFramework.EventSourcing.Migrations
{
    internal class StoredEventConfiguration : EntityTypeConfiguration<StoredEvent>
    {
        public StoredEventConfiguration(string schemaName)
        {
            if (schemaName == null) throw new ArgumentNullException(nameof(schemaName));

            ToTable("Events", schemaName);
            HasKey(x => new
            {
                x.AggregateId,
                x.Version
            });
        }
    }
}

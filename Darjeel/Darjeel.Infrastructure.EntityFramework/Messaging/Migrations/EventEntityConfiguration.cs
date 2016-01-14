using System;
using System.Data.Entity.ModelConfiguration;

namespace Darjeel.Infrastructure.EntityFramework.Messaging.Migrations
{
    internal class EventEntityConfiguration : EntityTypeConfiguration<EventEntity>
    {
        public EventEntityConfiguration(string schemaName)
        {
            if (schemaName == null) throw new ArgumentNullException(nameof(schemaName));

            ToTable("Events", schemaName);
        }
    }
}
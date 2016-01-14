using System;
using System.Data.Entity.ModelConfiguration;

namespace Darjeel.Infrastructure.EntityFramework.Messaging.Migrations
{
    internal class CommandEntityConfiguration : EntityTypeConfiguration<CommandEntity>
    {
        public CommandEntityConfiguration(string schemaName)
        {
            if (schemaName == null) throw new ArgumentNullException(nameof(schemaName));

            ToTable("Commands", schemaName);
        }
    }
}

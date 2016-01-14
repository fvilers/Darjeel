using Darjeel.Infrastructure.EntityFramework.Messaging.Migrations;
using System;
using System.Data.Entity;

namespace Darjeel.Infrastructure.EntityFramework.Messaging
{
    public class BusContext : ContextBase, IBusContext
    {
        public IDbSet<CommandEntity> Commands { get; set; }
        public IDbSet<EventEntity> Events { get; set; }

        private const string SchemaName = "bus";

        public BusContext()
            : base("Bus")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new ArgumentNullException(nameof(modelBuilder));

            modelBuilder.Configurations.Add(new CommandEntityConfiguration(SchemaName));
            modelBuilder.Configurations.Add(new EventEntityConfiguration(SchemaName));
        }
    }
}
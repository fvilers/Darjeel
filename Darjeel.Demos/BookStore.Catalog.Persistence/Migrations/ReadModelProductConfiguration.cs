using BookStore.Catalog.Persistence.ReadModels;
using System;
using System.Data.Entity.ModelConfiguration;

namespace BookStore.Catalog.Persistence.Migrations
{
    internal class ReadModelProductConfiguration : EntityTypeConfiguration<ReadModelProduct>
    {
        public ReadModelProductConfiguration(string schemaName)
        {
            if (schemaName == null) throw new ArgumentNullException(nameof(schemaName));

            ToTable("Products", schemaName);
            HasKey(x => x.AggregateId);
        }
    }
}

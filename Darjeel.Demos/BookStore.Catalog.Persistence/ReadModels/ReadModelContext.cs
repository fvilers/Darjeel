using BookStore.Catalog.Persistence.Migrations;
using Darjeel.Infrastructure.EntityFramework;
using System;
using System.Data.Entity;

namespace BookStore.Catalog.Persistence.ReadModels
{
    public class ReadModelContext : ContextBase, IReadModelContext
    {
        public IDbSet<Product> Products { get; set; }

        private const string SchemaName = "catalog";

        public ReadModelContext()
            : base("Catalog")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new ArgumentNullException(nameof(modelBuilder));

            modelBuilder.Configurations.Add(new ProductConfiguration(SchemaName));
        }
    }
}

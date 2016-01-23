using System;
using System.Data.Entity;

namespace Darjeel.EntityFramework
{
    public abstract class ContextBase : DbContext, IContext
    {
        protected ContextBase(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public DbContextTransaction BeginTransaction()
        {
            return Database.BeginTransaction();
        }

        public void SetValues<TEntity>(TEntity original, TEntity entity)
            where TEntity : class
        {
            if (original == null) throw new ArgumentNullException(nameof(original));
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Entry(original).CurrentValues.SetValues(entity);
        }
    }
}
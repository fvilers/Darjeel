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

        public bool IsDetached<TEntity>(TEntity entity)
            where TEntity : class
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var entry = Entry(entity);

            return entry.State == EntityState.Detached;
        }

        public DbContextTransaction BeginTransaction()
        {
            return Database.BeginTransaction();
        }
    }
}
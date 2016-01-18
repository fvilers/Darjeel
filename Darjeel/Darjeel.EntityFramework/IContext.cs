using Darjeel.Persistence;
using System;
using System.Data.Entity;

namespace Darjeel.EntityFramework
{
    public interface IContext : IUnitOfWork, IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        bool IsDetached<TEntity>(TEntity entity) where TEntity : class;
        DbContextTransaction BeginTransaction();
    }
}
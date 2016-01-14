using Darjeel.Infrastructure.Persistence;
using System;
using System.Data.Entity;

namespace Darjeel.Infrastructure.EntityFramework
{
    public interface IContext : IUnitOfWork, IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        bool IsDetached<TEntity>(TEntity entity) where TEntity : class;
        DbContextTransaction BeginTransaction();
    }
}
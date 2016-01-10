using Darjeel.Infrastructure.EntityFramework;
using System.Data.Entity;

namespace BookStore.Catalog.Persistence.ReadModels
{
    public interface IReadModelContext : IContext
    {
        IDbSet<Product> Products { get; }
    }
}
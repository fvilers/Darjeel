using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Darjeel.Infrastructure.Sagas
{
    public interface ISagaRepository<T> : IDisposable
        where T : class, ISaga
    {
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task StoreAsync(T saga);
    }
}
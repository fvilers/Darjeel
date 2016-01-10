using System.Threading.Tasks;

namespace Darjeel.Infrastructure.Persistence
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}
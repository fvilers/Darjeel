using System.Threading.Tasks;

namespace Darjeel.Persistence
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Catalog.ReadModels
{
    public interface IReadModelProductDao
    {
        Task<IEnumerable<IReadModelProduct>> FindAsync();
    }
}
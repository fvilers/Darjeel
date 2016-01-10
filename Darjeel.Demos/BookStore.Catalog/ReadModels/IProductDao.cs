using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Catalog.ReadModels
{
    public interface IProductDao
    {
        Task<IEnumerable<IProduct>> FindAsync();
    }
}
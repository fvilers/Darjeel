using BookStore.Catalog.ReadModels;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace BookStore.Catalog.UI.Controllers
{
    [RoutePrefix("api/catalog/products")]
    public class ProductController : ApiController
    {
        private readonly IProductDao _dao;

        public ProductController(IProductDao dao)
        {
            if (dao == null) throw new ArgumentNullException(nameof(dao));
            _dao = dao;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Find()
        {
            var products = await _dao.FindAsync();

            return Ok(products);
        }
    }
}

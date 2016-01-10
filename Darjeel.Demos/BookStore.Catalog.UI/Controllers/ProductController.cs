using System.Web.Http;

namespace BookStore.Catalog.UI.Controllers
{
    [RoutePrefix("api/catalog/products")]
    public class ProductController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult Find()
        {
            return Ok();
        }
    }
}

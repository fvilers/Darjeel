using BookStore.Catalog.Commands;
using BookStore.Catalog.ReadModels;
using Darjeel.Infrastructure.Messaging;
using Darjeel.Infrastructure.Messaging.Extensions;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace BookStore.Catalog.UI.Controllers
{
    [RoutePrefix("api/catalog/products")]
    public class ProductController : ApiController
    {
        private readonly IReadModelProductDao _dao;
        private readonly ICommandBus _bus;

        public ProductController(IReadModelProductDao dao, ICommandBus bus)
        {
            if (dao == null) throw new ArgumentNullException(nameof(dao));
            if (bus == null) throw new ArgumentNullException(nameof(bus));
            _dao = dao;
            _bus = bus;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Find()
        {
            var products = await _dao.FindAsync();

            return Ok(products);
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create(CreateProduct command)
        {
            await _bus.SendAsync(command);

            return StatusCode(HttpStatusCode.Created);
        }
    }
}

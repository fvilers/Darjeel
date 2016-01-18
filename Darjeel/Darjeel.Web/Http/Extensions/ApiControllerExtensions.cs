using System;
using System.Web.Http;

namespace Darjeel.Web.Http.Extensions
{
    public static class ApiControllerExtensions
    {
        public static AcceptedResult Accepted(this ApiController controller)
        {
            if (controller == null) throw new ArgumentNullException(nameof(controller));

            return new AcceptedResult(controller);
        }
    }
}

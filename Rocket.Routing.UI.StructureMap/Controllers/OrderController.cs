using System.Web.Http;

using Rocket.Routing;

namespace Routing.Controllers
{
    [RoutePrefix("api/orders")]
    public class OrderController : ApiController
    {
        [HttpGet]
        [VersionedRoute("", 1, isLatest: false)]
        public IHttpActionResult OrdersV1()
        {
            return Json("{version:1}");
        }

        [HttpGet]
        [VersionedRoute("", 2, isLatest: true)]
        public IHttpActionResult OrdersV2()
        {
            return Json("{version:2}");
        }
    }
}
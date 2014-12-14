using System.Web.Http;

using Rocket.Routing;

namespace Routing.Controllers
{
    [VersionedRoute("api/order", 2, isLatest: true)]
    public class OrderV2Controller : ApiController
    {
        [HttpGet]
        public IHttpActionResult Orders()
        {
            return Json("{version:2}");
        }
    }
}
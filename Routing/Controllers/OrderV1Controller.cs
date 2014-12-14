using System.Web.Http;

using Rocket.Routing;

namespace Routing.Controllers
{
	[VersionedRoute("api/order", 1, isLatest: true)]
	public class OrderV1Controller : ApiController
	{
		[HttpGet]
		public IHttpActionResult Orders()
		{
			return Json("{version:1}");
		}
	}
}
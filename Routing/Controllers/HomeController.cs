using System.Web.Mvc;

namespace Routing.Controllers
{
	// http://bitoftech.net/2013/12/16/asp-net-web-api-versioning-accept-header-query-string/
	// http://weblogs.asp.net/jongalloway/looking-at-asp-net-mvc-5-1-and-web-api-2-1-part-2-attribute-routing-with-custom-constraints

	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Title = "Home Page";

			return View();
		}
	}
}

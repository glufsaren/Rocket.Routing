using System.Web;
using System.Web.Mvc;

namespace Rocket.Routing.UI.StructureMap
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

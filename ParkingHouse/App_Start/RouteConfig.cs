using System.Web.Mvc;
using System.Web.Routing;

namespace ParkingHouse
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "GetTotal",
               url: "{controller}/{action}/{sum}/{carsTotal}",
               defaults: new { controller = "Parking", action = "List" }
           );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Parking", action = "List", id = UrlParameter.Optional}
            );


        }
    }
}
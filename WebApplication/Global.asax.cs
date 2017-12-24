using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using DB;
using SearchEngineForTrip;

namespace WebApplication
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            var init = new DBInitializer();
            Database.SetInitializer(init);
            init.InitializeDatabase(new DB.BDContext());
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
       
    }
}
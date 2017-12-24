using System.Web.Http;

namespace SearchEngineForTrip
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

           
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional}
            );
            // Setup json serialization to serialize classes to camel (std. Json format)
            
            GlobalConfiguration.Configuration.Formatters
                .Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);  
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling 
                = Newtonsoft.Json.ReferenceLoopHandling.Serialize;     
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling 
                = Newtonsoft.Json.PreserveReferencesHandling.Objects;
        }
    }
}
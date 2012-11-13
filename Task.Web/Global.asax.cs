using System.Web.Mvc;
using System.Web.Routing;
using Task.Infrastructure.Logging;
using Task.Infrastructure.Ninject;
using Task.Repositories.NinjectModules;
using Task.Services.NinjectModules;
using Task.Web.Ninject;

namespace Task.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            CreateMappings.Create();
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
            Locator.Init(new RepositoryModule(), new ServiceModule());
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            Logger.Log("Components are initialized");
        }
    }
}
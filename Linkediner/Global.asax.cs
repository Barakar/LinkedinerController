using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using Linkediner.DI;

namespace Linkediner
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var windsorContainer = new WindsorContainer();
            windsorContainer.Install(new WebWindsorInstaller());

            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator),
                                    new WindsorHttpControllerActivator(windsorContainer));
        }
    }
}

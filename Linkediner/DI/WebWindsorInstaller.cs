using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Linkediner.Controllers;
using Linkediner.DAL;
using Linkediner.HtmlHandlers;
using Linkediner.Interfaces;

namespace Linkediner.DI
{
    internal class WebWindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var fetcher = new HtmlFetcher();
            fetcher.AddHeader("Accept-Encoding", "gzip, deflate, sdch, br");

            container.Register(Component.For<IHtmlFetcher>().Instance(fetcher));

            container.Register(Component.For<ILinkedinParser>().ImplementedBy<LinkedinParser>());

            //TODO: move to config.
            var sqLiteDataAccessor = new SQLiteDataAccessor(@"C:\DB\Test.sqlite");

            container.Register(Component.For<IDataAccessor>().Instance(sqLiteDataAccessor));

            container.Register(Component.For<LinkedinerController>().LifestylePerWebRequest());


        }
    }
}
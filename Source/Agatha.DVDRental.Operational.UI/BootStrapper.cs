using Agatha.DVDRental.Catalogue.Catalogue;
using Agatha.DVDRental.Catalogue.Infrastructure;
using NServiceBus;
using Raven.Client;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace Agatha.DVDRental.Operational.UI
{
    public class BootStrapper
    {
        public static void ConfigureDependencies()
        {
            ObjectFactory.Initialize(x =>
                                         {
                                             x.AddRegistry<ControllerRegistry>();

                                         });
        }

        public class ControllerRegistry : Registry
        {
            public ControllerRegistry()
            {               
                For<IDocumentSession>().LifecycleIs(new HttpSessionLifecycle()).Use(DocumentStoreFactory.DocumentStore.OpenSession);

                For<IFilmRepository>().Use<FilmRepository>();

               var bus = NServiceBus.Configure.WithWeb()
                   .DefiningCommandsAs(t => t.Namespace != null && t.Namespace.StartsWith("Agatha.DVDRental.Fulfillment.Contracts"))
                   .DefaultBuilder()
                   .Log4Net()
                   .XmlSerializer()
                   .MsmqTransport()
                   .UnicastBus()
                   .SendOnly();

               For<IBus>().Use(bus);
            }
        }
    }
}
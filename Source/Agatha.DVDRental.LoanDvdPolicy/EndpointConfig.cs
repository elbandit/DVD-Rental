using Agatha.DVDRental.Domain.Films;
using Agatha.DVDRental.Infrastructure;
using NServiceBus;
using Raven.Client;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Agatha.DVDRental.LoanDvdPolicy 
{
    /*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://nservicebus.com/GenericHost.aspx
	*/

    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
        public void Init()
        {

            ObjectFactory.Initialize(x =>
                                         {
                                             x.AddRegistry<ControllerRegistry>();

                                         });

            Configure.With()
                .StructureMapBuilder()                
                .DefiningEventsAs(t => t.Namespace != null && t.Namespace.StartsWith("Agatha.DVDRental.Messages"));
        }

        public class ControllerRegistry : Registry
        {

            public ControllerRegistry()
            {

                // Repositories
                ForRequestedType<IFilmRepository>().TheDefault.Is.OfConcreteType
                    <FilmRepository>();

                For<IDocumentStore>().Singleton()
                               .Use(DocumentStoreFactory.DocumentStore);

                For<IDocumentSession>()
                    .Use(ctx => ctx.GetInstance<IDocumentStore>()
                    .OpenSession());
            }
        }
    }
}
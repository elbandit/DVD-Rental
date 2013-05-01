using Agatha.DVDRental.Catalogue.Catalogue;
using Agatha.DVDRental.Catalogue.Infrastructure;
using Agatha.DVDRental.Domain.RentalLists;
using Agatha.DVDRental.Infrastructure;
using Agatha.DVDRental.Subscription.Infrastructure;
using Agatha.DVDRental.Subscription.Model.RentalHistory;
using Agatha.DVDRental.Subscription.Model.Subscriptions;
using Raven.Client;
using DocumentStoreFactory = Agatha.DVDRental.Infrastructure.DocumentStoreFactory;

namespace Agatha.DVDRental.AllocationPolicy 
{    
    using NServiceBus;
    using StructureMap;
    using StructureMap.Configuration.DSL;

	/*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://nservicebus.com/GenericHost.aspx
	*/
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, AsA_Publisher, IWantCustomInitialization
    {        
        public void Init()
        {
            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry<ControllerRegistry>();

            });

            Configure.With()
                .StructureMapBuilder()
                //this overrides the NServiceBus default convention of IEvent                
                .DefiningMessagesAs(t => t.Namespace != null && t.Namespace.Contains("Agatha.DVDRental.Messages"));

        }

        public class ControllerRegistry : Registry
        {
            public ControllerRegistry()
            {
                For<IFilmRepository>().Use<FilmRepository>();
                For<IRentalRequestRepository>().Use<RentalRequestRepository>();
                For<ISubscriptionRepository>().Use<SubscriptionRepository>();
                For<IRentalRepository>().Use<RentalRepository>();
                

                For<IDocumentStore>().Singleton()
                                .Use(DocumentStoreFactory.DocumentStore);
                
                For<IDocumentSession>()
                    .Use(ctx => ctx.GetInstance<IDocumentStore>()
                    .OpenSession());
                           
            }
        }
    }
}
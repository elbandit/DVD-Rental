using System;
using Agatha.DVDRental.Catalogue.Catalogue;
using Agatha.DVDRental.Catalogue.Infrastructure;
using Agatha.DVDRental.Subscription.Infrastructure;
using Agatha.DVDRental.Subscription.Model.Allocation;
using Agatha.DVDRental.Subscription.Model.RentalHistory;
using Agatha.DVDRental.Subscription.Model.RentalRequests;
using Agatha.DVDRental.Subscription.Model.Subscriptions;
using NServiceBus.UnitOfWork;
using Raven.Client;
using Raven.Client.Document;
using DocumentStoreFactory = Agatha.DVDRental.Subscription.Infrastructure.DocumentStoreFactory;

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
                .DefiningEventsAs(
                    t =>
                    t.Namespace != null && (t.Namespace.Contains("Agatha.DVDRental.Fulfillment.Contracts") ||
                    t.Namespace.Contains("Agatha.DVDRental.Subscription.Contracts")));

        }

        public class ControllerRegistry : Registry
        {
            public ControllerRegistry()
            {               
                var store = new DocumentStore { ConnectionStringName = "RavenDB" };
                store.ResourceManagerId = Guid.NewGuid();
                store.Initialize();

                For<IFilmRepository>().Use<FilmRepository>();
                For<IRentalRequestRepository>().Use<RentalRequestRepository>();
                For<ISubscriptionRepository>().Use<SubscriptionRepository>();
                For<IRentalRepository>().Use<RentalRepository>();
                For<IAllocationRepository>().Use<AllocationRepository>();

                For<IDocumentStore>()
                    .Singleton()
                    .Use(store);

                For<IDocumentSession>()
                    .Use(ctx => ctx.GetInstance<IDocumentStore>()
                                      .OpenSession());

                For<IManageUnitsOfWork>()
                    .Use<RavenUnitOfWork>();
                           
            }
        }
    }
}
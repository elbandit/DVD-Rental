using System;
using Agatha.DVDRental.Fulfillment.Infrastructure;
using Agatha.DVDRental.Fulfillment.Model.Fulfilment;
using Agatha.DVDRental.Fulfillment.Model.Stock;
using NServiceBus.UnitOfWork;
using Raven.Client;
using Raven.Client.Document;

namespace Agatha.DVDRental.FulfillmentPolicy 
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
                .DefiningMessagesAs(
                    t => t.Namespace != null && (t.Namespace.Contains("Agatha.DVDRental.Fulfillment.Contracts") ||
                         t.Namespace.Contains("Agatha.DVDRental.Subscription.Contracts")));
            //.DefiningMessagesAs(t => t.Namespace != null && t.Namespace.Contains("Agatha.DVDRental.Subscription.Contracts"));

        }

        public class ControllerRegistry : Registry
        {
            public ControllerRegistry()
            {
                var store = new DocumentStore { ConnectionStringName = "RavenDB" };
                store.ResourceManagerId = Guid.NewGuid();
                store.Initialize();

                For<IDvdRepository>().Use<DvdRepository>();
                For<IFulfilmentRepository>().Use<FulfilmentRepository>();

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
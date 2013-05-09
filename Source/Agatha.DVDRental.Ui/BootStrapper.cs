using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Agatha.DVDRental.Catalogue.Catalogue;
using Agatha.DVDRental.Catalogue.Infrastructure;
using Agatha.DVDRental.Subscription.Infrastructure;
using Agatha.DVDRental.Subscription.Model.RentalRequests;
using Agatha.DVDRental.Subscription.Model.Subscriptions;
using NServiceBus;
using Raven.Client;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace Agatha.DVDRental.Ui
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

                For<ISubscriptionRepository>().Use<SubscriptionRepository>();
                For<IRentalRequestRepository>().Use<RentalRequestRepository>();

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
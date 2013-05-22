using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Fulfillment.Contracts;
using Agatha.DVDRental.Fulfillment.Contracts.Commands;
using NServiceBus;

namespace Agatha.DVDRental.FulfillmentPolicy.WebEventForwarding
{
    public class PublishThatAFilmHasBeenDispatchedHandler : IHandleMessages<PublishThatAFilmHasBeenDispatched>
    {
        private IBus _bus;

        public PublishThatAFilmHasBeenDispatchedHandler(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(PublishThatAFilmHasBeenDispatched message)
        {
            _bus.Publish(new FilmDispatched() {FilmId = message.FilmId,SubscriptionId = message.SubscriptionId});
        }
    }
}

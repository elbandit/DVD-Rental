using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Fulfillment.Contracts;
using Agatha.DVDRental.Fulfillment.Contracts.Commands;
using NServiceBus;

namespace Agatha.DVDRental.FulfillmentPolicy.WebEventForwarding
{
    public class PublishThatTheFilmIsBeingPickedHandler : IHandleMessages<PublishThatTheFilmIsBeingPicked>
    {
        private IBus _bus;

        public PublishThatTheFilmIsBeingPickedHandler(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(PublishThatTheFilmIsBeingPicked message)
        {
            _bus.Publish(new FilmBeingPicked() {FilmId = message.FilmId, SubscriptionId = message.SubscriptionId});
        }
    }
}

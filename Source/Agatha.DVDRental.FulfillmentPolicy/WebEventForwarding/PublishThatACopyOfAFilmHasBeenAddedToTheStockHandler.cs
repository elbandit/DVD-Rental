using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Fulfillment.Contracts;
using Agatha.DVDRental.Fulfillment.Contracts.Commands;
using NServiceBus;

namespace Agatha.DVDRental.FulfillmentPolicy.WebEventForwarding
{
    public class PublishThatACopyOfAFilmHasBeenAddedToTheStockHandler : IHandleMessages<PublishThatACopyOfAFilmHasBeenAddedToTheStock>
    {
        private IBus _bus;

        public PublishThatACopyOfAFilmHasBeenAddedToTheStockHandler(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(PublishThatACopyOfAFilmHasBeenAddedToTheStock message)
        {
            _bus.Publish(new ACopyOfAFilmHasBeenAddedToTheStock(){FilmId = message.FilmId});
        }
    }
}

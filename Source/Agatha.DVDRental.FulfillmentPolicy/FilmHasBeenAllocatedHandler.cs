using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Fulfillment.Model.Fulfilment;
using Agatha.DVDRental.Subscription.Contracts;
using NServiceBus;

namespace Agatha.DVDRental.FulfillmentPolicy
{
    public class FilmHasBeenAllocatedHandler : IHandleMessages<FilmHasBeenAllocated>
    {
        private IFulfilmentRepository _fulfilmentRepository;

        public FilmHasBeenAllocatedHandler(IFulfilmentRepository fulfilmentRepository)
        {
            _fulfilmentRepository = fulfilmentRepository;
        }

        public void Handle(FilmHasBeenAllocated message)
        {
            // Create a fulfillment request record for this.

            // Make sure we don't already have a request for this
            // Should this logic be in a service?
            _fulfilmentRepository.FindBy(message.FilmId, message.SubscriptionId);

            // create one
            var fulfilmentRequest = new FulfilmentRequest();  // event thrown

            _fulfilmentRepository.Add(fulfilmentRequest);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Fulfillment.Contracts;
using Agatha.DVDRental.Subscription.Model.RentalHistory;
using NServiceBus;

namespace Agatha.DVDRental.AllocationPolicy.FulfillmentIntegration
{
    public class FilmReturnedHandler : IHandleMessages<FilmReturned>
    {        
        private IRentalRepository _rentalRepository;
        private IBus _bus;

        public void Handle(FilmReturned message)
        {
            // Update rental history

            // start to allocate another
        }
    }
}

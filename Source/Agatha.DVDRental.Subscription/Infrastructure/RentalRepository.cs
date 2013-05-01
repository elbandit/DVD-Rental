using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Subscription.Model.RentalHistory;
using Raven.Client;

namespace Agatha.DVDRental.Subscription.Infrastructure
{
    public class RentalRepository : IRentalRepository
    {
        private readonly IDocumentSession _documentSession;
        
        public RentalRepository(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public CurrentPeriodRentals FindRentalsForCurrentPeriod()
        {
            return new CurrentPeriodRentals();
        }
    }
}
